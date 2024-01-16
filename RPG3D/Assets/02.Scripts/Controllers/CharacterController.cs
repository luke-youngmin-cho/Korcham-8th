using UnityEngine;

namespace RPG.Controllers
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] float _speed;
        Vector3 _velocity;
        Vector3 _accel;
        Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"))
                            .normalized * _speed;

            if (Input.GetKeyDown(KeyCode.Space) )
            {
                Jump();
            }
        }

        // 가속도 = 속도 / 시간
        // 속도 = 초기속도 + 가속도 * 시간
        private void FixedUpdate()
        {
            // 위치변화 = 속도 * 시간변화
            // 다음위치 = 현재위치 + 위치변화 = 현재위치 + 속도 * 시간변화
            transform.position += _velocity * Time.fixedDeltaTime;

            //if (IsGrounded())
            //{
            //
            //}
            //else
            //{
            //    _velocity += _accel * Time.fixedDeltaTime;
            //    _accel += Physics.gravity * Time.fixedDeltaTime; // 중력가속도
            //}
        }

        private bool IsGrounded()
        {
            return true;
        }

        // a = v / t
        // F = m * a -> a = F / m
        // I = F * t = m * a * t = m * v -> v = I / m
        private void AddForce(Vector3 force, ForceMode forceMode)
        {
            switch (forceMode)
            {
                case ForceMode.Force:
                    _accel += force / _rigidbody.mass;
                    break;
                case ForceMode.Acceleration:
                    _accel += force;
                    break;
                case ForceMode.Impulse:
                    _velocity += force / _rigidbody.mass;
                    break;
                case ForceMode.VelocityChange:
                    _velocity += force;
                    break;
                default:
                    break;
            }
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }
    }
}