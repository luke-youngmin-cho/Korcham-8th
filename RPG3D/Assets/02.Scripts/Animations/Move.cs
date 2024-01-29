using UnityEngine;
using System.Collections.Generic;

namespace RPG.Animations
{
    public class Move : StateMachineBehaviourBase
    {
        public override void Init(Controllers.CharacterController controller)
        {
            base.Init(controller);
            transitions = new Dictionary<State, System.Func<Animator, bool>>()
            {
                { State.Jump, (animator) =>
                {
                    return controller.inputCommmands[State.Jump] &&
                           animator.IsGrounded();
                }},
                { State.Fall, (animator) =>
                {
                    return animator.IsGrounded() == false;
                }},
                { State.Attack, (animator) =>
                {
                    return controller.inputCommmands[State.Attack];
                }},
            };
        }
    }
}
