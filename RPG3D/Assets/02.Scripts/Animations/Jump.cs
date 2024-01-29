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
        private float _force = 5.0f;
        private Rigidbody _rigidbody;

        public override void Init(Controllers.CharacterController controller)
        {
            base.Init(controller);

            transitions = new Dictionary<State, Func<Animator, bool>>()
            {
                { State.Fall, (animator) =>
                {
                    if (_rigidbody == null)
                        return false;

                    return _rigidbody.velocity.y <= 0;
                } }
            };
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.transform.position += Vector3.up * 0.15f;
            _rigidbody = animator.GetComponent<Rigidbody>();
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0.0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);
        }
    }
}
