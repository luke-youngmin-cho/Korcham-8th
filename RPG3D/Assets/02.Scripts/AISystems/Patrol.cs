using UnityEngine;

namespace RPG.AISystems
{
    /// <summary>
    /// 처음 위치기준 특정 반경 내에서 랜덤하게 움직이거나 / 특정 시간동안 멈춰서있는 행동
    /// 가만히 있을 비율에 따라서 가만히 있거나 움직임을 결정함.
    /// </summary>
    public class Patrol : Node
    {
        /// <summary>
        /// </summary>
        /// <param name="radius"> 정찰 반경 </param>
        /// <param name="idleTime"> 정찰 안하고 놀기로 결정했다면, 몇초동안 놀건지 </param>
        /// <param name="idleRatio"> 정찰 안하고 놀기로 결정할 확률 </param>
        public Patrol(BehaviourTree tree, float radius, float idleTime, float idleRatio) 
            : base(tree)
        {
            _origin = tree.transform.position;
            _radius = radius;
            _idleTime = idleTime;
            _idleRatio = idleRatio;
        }


        private Vector3 _origin;
        private float _radius;
        private float _idleTime;
        private float _idleTimer;
        private float _idleRatio; // 0.0f ~ 1.0f

        public override Result Invoke()
        {
            // if (노는 시간 끝났는지 & 정찰이 끝났는지)
            // {
            //      정찰 할건지 = idleRatio 를 가지고 랜덤하게 구함
            //      if (정찰 할거다) 
            //      {
            //          현재위치에서 radius 내의 랜덤한 좌표 구하고, 해당 위치로 길찾기 목표 설정
            //      }
            //      else (놀거다)
            //      {
            //          놀기 타이머설정.
            //      }
            // }
            // return Success

            if (_idleTimer > 0)
            {
                _idleTimer -= Time.deltaTime;
            }
            else if (blackboard.agent.remainingDistance <= blackboard.agent.stoppingDistance)
            {
                if (Random.Range(0.0f, 1.0f) < _idleRatio)
                {
                    //// x^2 + y^2 = r^2 ( r <= _radius )
                    //float r = Random.Range(0.0f, _radius);
                    //float x = Random.Range(-r, r);
                    //float y = Mathf.Sqrt(r * r - x * x);

                    Vector2 randomCoord = Random.insideUnitCircle * _radius;
                    blackboard.agent.SetDestination(_origin
                                                    + new Vector3(randomCoord.x, 0.0f, randomCoord.y));
                    blackboard.controller.speedGain = 1.0f;
                }
                else
                {
                    _idleTimer = _idleTime;
                    blackboard.controller.speedGain = 0.0f;
                }
            }
            
            return Result.Success;
        }
    }
}
