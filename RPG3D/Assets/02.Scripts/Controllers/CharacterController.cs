using RPG.Animations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Controllers
{
    public abstract class CharacterController : MonoBehaviour, IHp
    {
        public virtual float horizontal { get; set; }
        public virtual float vertical { get; set; }
        public virtual float speedGain { get; set; }

        public bool aiOn
        {
            get => _aiOn;
            set
            {
                if (_aiOn == value)
                    return;

                _aiOn = value;
                agent.enabled = value;
            }
        }
        private bool _aiOn;

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

        public Vector3 velocity
        {
            get => _velocity;
            set
            {
                _velocity = value;
            }
        }
        Vector3 _velocity;

        public Vector3 accel
        {
            get => _accel;
            set
            {
                _accel = value;
            }
        }
        Vector3 _accel;
        [SerializeField] private float _slopeAngle = 45.0f;
        [SerializeField] private float _step = 0.2f;
        [SerializeField] private float _footHeight = 0.1f;
        [SerializeField] private LayerMask _groundMask;
        Rigidbody _rigidbody;
        protected Animator animator;
        public NavMeshAgent agent { get; private set; }

        float _hp;
        float _hpMax = 100;
        float _atk = 10.0f;

        public Dictionary<State, bool> inputCommmands;
        public int weaponType
        {
            get => _weaponType;
            set
            {
                if (_weaponType == value)
                    return;

                _weaponType = value;
                animator.SetInteger("weapon", value);
            }
        }
        private int _weaponType;
        public bool isAttacking;
        public Coroutine comboStackResetCoroutine;

        //public delegate void OnHpChangedHandler(float value); // 대리자 타입 정의
        //public event OnHpChangedHandler onHpChanged; // 대리자 변수 선언
        // event 한정자 : 외부 클래스에서는 이 대리자를 += 혹은 -= 의 왼쪽에만 사용할 수 있도록 제한하는 한정자
        public event Action<float> onHpChanged;
        public event Action<float> onHpDepleted;
        public event Action<float> onHpRecovered;
        public event Action onHpMin;
        public event Action onHpMax;


        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }

        protected virtual void Start()
        {
            InitAnimatorBehaviours();
        }

        // protected 접근제한자 : 상속받은 자식 클래스만 접근 가능함.
        protected virtual void Update()
        {
            if (this.IsGrounded())
            {
                if (_aiOn)
                {
                    agent.speed = speedGain;
                    _velocity = new Vector3(Vector3.Dot(transform.right, agent.velocity),
                                            0.0f,
                                            Vector3.Dot(transform.forward, agent.velocity));
                }   
                else
                {
                    _velocity = new Vector3(horizontal, 0f, vertical).normalized * speedGain;
                }
            }

            animator.SetFloat("velocityX", _velocity.x);
            animator.SetFloat("velocityZ", _velocity.z);
        }

        // 가속도 = 속도 / 시간
        // 속도 = 초기속도 + 가속도 * 시간
        private void FixedUpdate()
        {
            // 위치변화 = 속도 * 시간변화
            // 다음위치 = 현재위치 + 위치변화 = 현재위치 + 속도 * 시간변화
            //transform.position += _velocity * Time.fixedDeltaTime;

            if (aiOn)
            {
                // AutoMove();
            }
            else
            {
                ManualMove();
            }
        }

        /// <summary>
        /// 수동 조작시 사용할 움직임
        /// </summary>
        private void ManualMove()
        {
            if (this.IsGrounded())
            {
                _accel.y = 0.0f;
                _velocity.y = 0.0f;
                RaycastHit hit;
                Vector3 expectedRel = Quaternion.LookRotation(transform.forward) * _velocity * Time.fixedDeltaTime;
                Vector3 expected = transform.position
                                   + expectedRel;

                Vector3 slopeTop = transform.position
                                   + Quaternion.AngleAxis(-45, transform.right)
                                   * Quaternion.LookRotation(transform.forward)
                                   * _velocity * Time.fixedDeltaTime;

                Vector3 slopeBottom = transform.position
                                   + Quaternion.AngleAxis(+45, transform.right)
                                   * Quaternion.LookRotation(transform.forward)
                                   * _velocity * Time.fixedDeltaTime;

                // 계단 확인, 윗쪽 경사확인, 아랫쪽 경사확인
                if (Physics.Raycast(expected + Vector3.up * _step,
                                    Vector3.down,
                                    out hit,
                                    _step * 2,
                                    _groundMask) ||
                    Physics.Linecast(slopeTop, expected, out hit, _groundMask) ||
                    Physics.Linecast(expected, slopeBottom, out hit, _groundMask))
                {
                    // NavMesh 확인 
                    if (NavMesh.SamplePosition(hit.point,
                                               out NavMeshHit navMeshHit,
                                               1.0f,
                                               NavMesh.AllAreas))
                    {
                        transform.position = navMeshHit.position;
                    }
                }

                Debug.DrawLine(slopeTop, expected, hit.collider ? Color.green : Color.red, 2.0f);
                Debug.DrawLine(expected, slopeBottom, hit.collider ? Color.green : Color.red, 2.0f);
            }
            else
            {
                _velocity += _accel * Time.fixedDeltaTime;
                _accel += Physics.gravity * Time.fixedDeltaTime;
            
                RaycastHit hit;
                Vector3 expectedRel = Quaternion.LookRotation(transform.forward) * _velocity * Time.fixedDeltaTime;
                Vector3 expected = transform.position
                                   + expectedRel;
                if (Physics.Linecast(transform.position,
                                     expected,
                                     out hit,
                                     _groundMask))
                {
                    // NavMesh 확인 
                    if (NavMesh.SamplePosition(hit.point,
                                               out NavMeshHit navMeshHit,
                                               1.0f,
                                               NavMesh.AllAreas))
                    {
                        transform.position = navMeshHit.position;
                    }
                }
                else
                {
                    transform.position += Quaternion.LookRotation(transform.forward) * _velocity * Time.fixedDeltaTime;
                }
            }
        }

        protected virtual void InitAnimatorBehaviours()
        {
            var behaviours = animator.GetBehaviours<StateMachineBehaviourBase>();
            foreach (var behaviour in behaviours)
            {
                behaviour.Init(this);
            }
        }

        // a = v / t
        // F = m * a -> a = F / m
        // I = F * t = m * a * t = m * v -> v = I / m
        public void AddForce(Vector3 force, ForceMode forceMode)
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


        private void OnAnimatorIK(int layerIndex)
        {
            FootIK();
        }

        private void FootIK()
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
            Ray ray;
            RaycastHit hit;

            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up * _step, Vector3.down);
            if (Physics.Raycast(ray, out hit, _step * 2.0f, _groundMask))
            {
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + Vector3.up * _footHeight);
            }

            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up * _step, Vector3.down);
            if (Physics.Raycast(ray, out hit, _step * 2.0f, _groundMask))
            {
                animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + Vector3.up * _footHeight);
            }
        }


        private void FootR() { }
        private void FootL() { }

        private void Hit(AnimationEvent e) 
        {
            isAttacking = false;

            if (e.objectReferenceParameter is SkillInfo)
            {
                SkillInfo skillInfo = (SkillInfo)e.objectReferenceParameter;

                RaycastHit[] hits =
                Physics.CapsuleCastAll(transform.position + Quaternion.LookRotation(transform.forward) * skillInfo.castingPoint1,
                                       transform.position + Quaternion.LookRotation(transform.forward) * skillInfo.castingPoint2,
                                       skillInfo.castingRadius,
                                       Quaternion.LookRotation(transform.forward) * skillInfo.castingDirection,
                                       skillInfo.castingMaxDistance,
                                       skillInfo.castingMask);

                for (int i = 0; i < skillInfo.maxTargets && i < hits.Length; i++)
                {
                    if (hits[i].collider.TryGetComponent(out IHp hp))
                    {
                        hp.DepleteHp(_atk * skillInfo.damageGain);
                    }
                }
            }
        }
    }
}