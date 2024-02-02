using UnityEngine;
using RPG.Animations;

namespace RPG.AISystems
{
    public class Attack : Node
    {
        public Attack(BehaviourTree tree, float range) : base(tree)
        {
            _range = range;
        }

        private float _range;

        public override Result Invoke()
        {
            // if (목표가 있으면서 공격범위 내에 있음)
            // {
            //      컨트롤러에 공격 명령
            //      공격중이므로 Running 반환
            // }
            // else
            // {
            //      공격 불가능하므로 Failure 반환
            // }

            if (blackboard.target &&
                Vector3.Distance(blackboard.transform.position, blackboard.target.position) <= _range)
            {
                blackboard.controller.inputCommmands[State.Attack] = true;
                Debug.Log("Attack");
                return Result.Running;
            }

            blackboard.controller.inputCommmands[State.Attack] = false;
            Debug.Log("Cancel Attack");
            return Result.Failure;
        }
    }
}
