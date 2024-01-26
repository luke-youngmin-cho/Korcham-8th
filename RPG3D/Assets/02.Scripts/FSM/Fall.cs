using UnityEngine;

namespace RPG.FSM
{
    public class Fall : StateBase
    {
        public override State state => State.Fall;

        public override State OnUpdate(Animator animator)
        {
            State next = base.OnUpdate(animator);

            if (animator.IsGrounded())
                next = State.Move;

            return next;
        }
    }
}
