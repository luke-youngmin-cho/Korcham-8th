using UnityEngine;
using System.Collections.Generic;

namespace RPG.Animations
{
    public class Fall :StateMachineBehaviourBase
    {
        public override void Init(Controllers.CharacterController controller)
        {
            base.Init(controller);

            transitions = new Dictionary<State, System.Func<Animator, bool>>()
            {
                { State.Move, (animator) =>
                {
                    return animator.IsGrounded();
                } },
            };
        }
    }
}
