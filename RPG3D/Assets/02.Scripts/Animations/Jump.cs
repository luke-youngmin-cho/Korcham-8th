using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Animations
{
    public class Jump : StateMachineBehaviourBase
    {
        private float _force = 3.0f;

        public override void Init(Controllers.CharacterController controller)
        {
            base.Init(controller);

            transitions = new Dictionary<State, Func<Animator, bool>>()
            {
                { State.Fall, (animator) =>
                {
                    return controller.velocity.y <= 0;
                }}
            };
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.transform.position += Vector3.up * 0.2f;
            controller.velocity = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z);
            controller.AddForce(Vector3.up * _force, ForceMode.Impulse);
        }
    }
}
