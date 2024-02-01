using UnityEngine;

namespace RPG.AISystems
{
    public class Seek : Node
    {
        public Seek(BehaviourTree tree, float radius, float height, float angle, LayerMask targetMask, float maxDistance) : base(tree)
        {
            _radius = radius;
            _height = height;
            _angle = angle;
            _targetMask = targetMask;
            _maxDistance = maxDistance;
        }

        private float _radius;
        private float _height;
        private float _angle;
        private LayerMask _targetMask;
        private float _maxDistance;

        public override Result Invoke()
        {
            GizmosDrawer.DrawArc(blackboard.transform, _radius, _angle, Color.yellow);

            if (blackboard.target)
            {
                float distance = Vector3.Distance(blackboard.transform.position, blackboard.target.position);

                if (distance <= blackboard.agent.stoppingDistance)
                {
                    return Result.Success;
                }
                else if (distance <= _maxDistance)
                {
                    blackboard.agent.SetDestination(blackboard.target.position);
                    blackboard.controller.speedGain = 1.0f;
                    return Result.Running;
                }
                else
                {
                    blackboard.target = null;
                    blackboard.agent.ResetPath();
                    return Result.Failure;
                }
            }
            else
            {
                Collider[] cols =
                Physics.OverlapCapsule(blackboard.transform.position,
                                   blackboard.transform.position + Vector3.up * _height,
                                   _radius,
                                   _targetMask);

                if (cols.Length > 0)
                {
                    if (IsInSight(cols[0].transform))
                    {
                        blackboard.target = cols[0].transform;
                        blackboard.agent.SetDestination(blackboard.target.position);
                        blackboard.controller.speedGain = 1.0f;
                        return Result.Running;
                    }
                }
            }

            return Result.Failure;
        }

        private bool IsInSight(Transform target)
        {
            Vector3 origin = blackboard.transform.position; // 내 위치
            Vector3 forward = blackboard.transform.forward; // 내 기준 앞쪽 방향벡터
            Vector3 lookDir = (target.position - origin).normalized; // 타겟을 바라보는 방향벡터
            float theta = Mathf.Acos(Vector3.Dot(forward, lookDir)) * Mathf.Rad2Deg;

            // 사이각 내에 있으면
            if (theta < _angle / 2.0f)
            {
                if (Physics.Raycast(origin + Vector3.up * _height / 2.0f,
                                    lookDir,
                                    out RaycastHit hit,
                                    Vector3.Distance(origin, target.position),
                                    _targetMask))
                {
                    return hit.collider.transform == target;
                }
            }

            return false;
        }
    }
}
