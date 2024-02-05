using UnityEngine;
using RPG.Animations;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using RPG.InputSystems;
using Cinemachine;

namespace RPG.Controllers
{
    public class PlayerController : CharacterController, PlayerInputActions.IBattleFieldActions
    {
        public GameObject twoHandedSword;
        PlayerInputActions _inputActions;

        [Header("Camera")]
        [SerializeField] CinemachineVirtualCamera _vCam;
        [SerializeField] float _rotateSpeedY;
        [SerializeField] float _rotateSpeedX;
        [SerializeField] float _angleXMin = -8.0f;
        [SerializeField] float _angleXMax = 45.0f;
        [SerializeField] float _fovMin = 20.0f;
        [SerializeField] float _fovMax = 80.0f;
        [SerializeField] float _scrollThreshold; // 스크롤 임계점, <-> 민감도
        [SerializeField] float _scrollSpeed;


        protected override void Start()
        {
            base.Start();
            aiOn = false;
            /*
            PlayerInput playerInput = GetComponent<PlayerInput>();
            playerInput.SwitchCurrentActionMap("BattleField");
            var jump = playerInput.currentActionMap.FindAction("Jump");
            jump.started += OnJump;
            jump.performed += OnJump;
            jump.canceled += OnJump;
            */
            _inputActions = new PlayerInputActions();
            _inputActions.BattleField.SetCallbacks(this);
            _inputActions.Enable();

            inputCommmands = new Dictionary<State, bool>()
            {
                { State.Jump, false },
                { State.Attack, false }
            };
        }

        protected override void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            speedGain = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                weaponType = 0;
                twoHandedSword.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                weaponType = 1;
                twoHandedSword.SetActive(true);
            }

            inputCommmands[State.Attack] = isAttacking == false &&
                                           Input.GetMouseButton(0);

            base.Update();
        }

        private void LateUpdate()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, mouseX * _rotateSpeedY * Time.deltaTime, Space.World);
            _vCam.Follow.Rotate(Vector3.left, mouseY * _rotateSpeedX * Time.deltaTime, Space.Self);
            _vCam.Follow.localRotation 
                = Quaternion.Euler(ClampAngle(_vCam.Follow.localEulerAngles.x, _angleXMin, _angleXMax),
                                   0f,
                                   0f);

            if (Mathf.Abs(Input.mouseScrollDelta.y) >= _scrollThreshold)
            {
                _vCam.m_Lens.FieldOfView -= Input.mouseScrollDelta.y * _scrollSpeed * Time.deltaTime;
                _vCam.m_Lens.FieldOfView = Mathf.Clamp(_vCam.m_Lens.FieldOfView, _fovMin, _fovMax);
            }
        }

        private float ClampAngle(float angle, float min, float max)
        {
            angle = (angle + 360f) % 360f;
            min = (min + 360f) % 360f;
            max = (max + 360f) % 360f;

            if (min < max)
            {
                return Mathf.Clamp(angle, min, max);
            }    
            else if (angle > min || angle < max)
            {
                return angle;
            }
            else
            {
                float diffMin = Mathf.Min(Mathf.Abs(angle - min), Mathf.Abs(angle - 360f - min));
                float diffMax = Mathf.Min(Mathf.Abs(angle - max), Mathf.Abs(angle + 360f - max));

                return diffMin < diffMax ? min : max;
            }
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            horizontal = value.x;
            vertical = value.y;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
                inputCommmands[State.Jump] = true;

            if (context.canceled)
                inputCommmands[State.Jump] = false;
        }
    }
}
