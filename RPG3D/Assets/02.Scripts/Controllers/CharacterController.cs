using System;
using System.Collections;
using UnityEngine;

namespace RPG.Controllers
{
    public abstract class CharacterController : MonoBehaviour, IHp
    {
        public virtual float horizontal { get; set; }
        public virtual float vertical { get; set; }
        public virtual float speedGain { get; set; }

        public float hpValue
        {
            get
            {
                return _hp;
            }
            set
            {
                value = Mathf.Clamp(value, 0, _hpMax);

                if (_hp == value)
                    return;
             
                _hp = value;

                if (value <= 0)
                    onHpMin?.Invoke();
                else if (value >= _hpMax)
                    onHpMax?.Invoke();

                //onHpChanged(value);
                //onHpChanged.Invoke(value);
                onHpChanged?.Invoke(value); // null check 연산자 : 왼쪽 피연산자가 null 일경우 null 반환
            }
        }

        public float hpMax => _hpMax;

        Vector3 _velocity;
        Vector3 _accel;
        Rigidbody _rigidbody;
        Animator _animator;
        float _hp;
        float _hpMax = 100;
        //public delegate void OnHpChangedHandler(float value); // 대리자 타입 정의
        //public event OnHpChangedHandler onHpChanged; // 대리자 변수 선언
        // event 한정자 : 외부 클래스에서는 이 대리자를 += 혹은 -= 의 왼쪽에만 사용할 수 있도록 제한하는 한정자
        public event Action<float> onHpChanged;
        public event Action<float> onHpDepleted;
        public event Action<float> onHpRecovered;
        public event Action onHpMin;
        public event Action onHpMax;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            
        }

        // protected 접근제한자 : 상속받은 자식 클래스만 접근 가능함.
        protected virtual void Update()
        {
            _velocity = new Vector3(horizontal, 0f, vertical)
                            .normalized * speedGain;
            _animator.SetFloat("velocityX", _velocity.x);
            _animator.SetFloat("velocityZ", _velocity.z);
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

        public void DepleteHp(float amount)
        {
            hpValue -= amount;
            onHpDepleted?.Invoke(amount);
        }

        public void RecoverHp(float amount)
        {
            hpValue += amount;
            onHpRecovered?.Invoke(amount);
        }

        private void FootR() { }
        private void FootL() { }


        public void Test()
        {
            IEnumerator enumerator = C_Test();
            StartCoroutine(enumerator);
            StartCoroutine(new Enumerator());
        }

        IEnumerator C_Test()
        {
            int count = 5;
            while (count > 0)
            {
                count--;
                yield return null;
            }

            yield return new WaitForSeconds(5);

            Debug.Log("Passed 5 seconds");

            yield return new WaitForEndOfFrame();

            Debug.Log("Finished frame...");
        }

        struct Enumerator : IEnumerator
        {
            public object Current => throw new NotImplementedException();

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}