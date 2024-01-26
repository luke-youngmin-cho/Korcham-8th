using UnityEngine;

namespace RPG.FSM
{
    public class Jump : StateBase
    {
        public override State state => State.Jump;
        private float _force = 5.0f;
        private Rigidbody _rigidbody;

        public override void OnEnter(Animator animator)
        {
            base.OnEnter(animator);
            _rigidbody = animator.GetComponent<Rigidbody>();
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0.0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);
        }

        public override State OnUpdate(Animator animator)
        {
            State next = base.OnUpdate(animator);

            if (_rigidbody.velocity.y <= 0)
            {
                next = State.Fall;
            }

            return next;
        }
    }
}
