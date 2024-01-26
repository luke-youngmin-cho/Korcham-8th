using UnityEngine;

namespace RPG.FSM
{
    public class Move : StateBase
    {
        public override State state => State.Move;

        public override State OnUpdate(Animator animator)
        {
            State next = base.OnUpdate(animator);

            if (animator.IsGrounded() == false)
                next = State.Fall;

            return next;
        }
    }
}
