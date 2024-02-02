using Unity.VisualScripting;
using UnityEngine;

namespace RPG.AISystems
{
    /// <summary>
    /// 목표를 찾고, 쫒는 행동 노드
    /// </summary>
    public class Seek : Node
    {
        /// <summary>
        /// </summary>
        /// <param name="tree"> 이 노드를 포함하는 트리 </param>
        /// <param name="radius"> 목표 감지 반경 </param>
        /// <param name="height"> 목표 감지 높이  </param>
        /// <param name="angle"> 시야각 </param>
        /// <param name="targetMask"> 목표 감지 마스크 </param>
        /// <param name="maxDistance"> 목표 추적을 지속하는 최대 거리 </param>
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


        /// <summary>
        /// 목표를 찾고, 찾은 목표에 도착할 때 까지 쫒아감.
        /// 쫒아가는 도중에 목표가 특정 거리를 벗어나면 추적을 중단함.
        /// </summary>
        public override Result Invoke()
        {
            GizmosDrawer.DrawArc(blackboard.transform, _radius, _angle, Color.yellow); // 기즈모에 탐지 시야각 그림.

            // 이미 감지된 목표가 있다면
            if (blackboard.target)
            {
                // 목표 까지의 거리
                float distance = Vector3.Distance(blackboard.transform.position, blackboard.target.position);

                // 목표에 도달 했을 때
                if (distance <= blackboard.agent.stoppingDistance)
                {
                    return Result.Success; // 목표 추적 성공
                }
                // 목표 추적 중
                else if (distance <= _maxDistance)
                {
                    blackboard.agent.SetDestination(blackboard.target.position); // 길찾기 목표 위치 갱신
                    blackboard.controller.speedGain = 1.0f; // 속도 설정
                    return Result.Running; // 목표 추적 다음 프레임에도 계속 이어서 해야함.
                }
                // 목표가 추적 최대 범위를 벗어났을 때
                else
                {
                    blackboard.target = null; // 목표 지움.
                    blackboard.agent.ResetPath(); // 길찾기 경로 지움.
                    return Result.Failure; // 목표 추적 실패
                }
            }
            // 감지된 목표가 없으면
            else
            {
                // 캡슐 반경 감지
                Collider[] cols =
                Physics.OverlapCapsule(blackboard.transform.position,
                                       blackboard.transform.position + Vector3.up * _height,
                                       _radius,
                                       _targetMask);

                // 감지된 목표가 있는지?
                if (cols.Length > 0)
                {
                    // 감지된 목표가 시야범위 내에 있는지?
                    if (IsInSight(cols[0].transform))
                    {
                        blackboard.target = cols[0].transform; // 목표 설정
                        blackboard.agent.SetDestination(blackboard.target.position); // 길찾기 목표 위치 설정
                        blackboard.controller.speedGain = 1.0f; // 속도 설정
                        return Result.Running;
                    }
                }
            }

            return Result.Failure;
        }

        /// <summary>
        /// 시야각 내에 있는지 판별.
        /// </summary>
        /// <param name="target"> 판별 대상 </param>
        /// <returns> true: 시야각내에 있음, false: 시야각내에 없음 </returns>
        private bool IsInSight(Transform target)
        {
            Vector3 origin = blackboard.transform.position; // 내 위치
            Vector3 forward = blackboard.transform.forward; // 내 기준 앞쪽 방향벡터
            Vector3 lookDir = (target.position - origin).normalized; // 타겟을 바라보는 방향벡터
            float theta = Mathf.Acos(Vector3.Dot(forward, lookDir)) * Mathf.Rad2Deg; // 사이각 구함

            // 목표와의 사이각이 시야각 내에 있으면
            if (theta < _angle / 2.0f)
            {
                // 목표와 나 사이에 장애물이 없는지 선을 쏘아서 확인
                if (Physics.Raycast(origin + Vector3.up * _height / 2.0f,
                                    lookDir,
                                    out RaycastHit hit,
                                    Vector3.Distance(origin, target.position),
                                    _targetMask))
                {
                    return hit.collider.transform == target; // 선에 맞은 대상과 시야각 내 찾은 대상이 맞다면 OK
                }
            }

            return false;
        }
    }
}
