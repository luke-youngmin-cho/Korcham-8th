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
        [Header("Weapon")]
        public GameObject twoHandedSword;
        public override int weaponType 
        { 
            get => base.weaponType;
            set
            {
                base.weaponType = value;

                switch (value)
                {
                    case 0:
                        {
                            twoHandedSword.SetActive(false);
                        }
                        break;
                    case 1:
                        {
                            twoHandedSword.SetActive(true);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

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

        PlayerInputActions _inputActions;
        float _mouseX, _mouseY, _mouseScroll;


        protected override void Start()
        {
            base.Start();
            aiOn = false;
            _inputActions = new PlayerInputActions();
            _inputActions.BattleField.SetCallbacks(this);
            _inputActions.Enable();

            inputCommmands = new Dictionary<State, bool>()
            {
                { State.Jump, false },
                { State.Attack, false }
            };
        }

        private void LateUpdate()
        {
            UpdateSight();
            UpdateZoom();
        }

        private void UpdateSight()
        {
            transform.Rotate(Vector3.up, _mouseX * _rotateSpeedY, Space.World);
            _vCam.Follow.Rotate(Vector3.left, _mouseY * _rotateSpeedX, Space.Self);
            _vCam.Follow.localRotation
                = Quaternion.Euler(_vCam.Follow.localEulerAngles.x.ClampAsNormalizedAngle(_angleXMin, _angleXMax), 0.0f, 0.0f);
        }
        
        private void UpdateZoom()
        {
            if (Mathf.Abs(_mouseScroll) < _scrollThreshold)
                return;

            _vCam.m_Lens.FieldOfView -= _mouseScroll * _scrollSpeed;
            _vCam.m_Lens.FieldOfView = Mathf.Clamp(_vCam.m_Lens.FieldOfView, _fovMin, _fovMax);
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
            else if (context.canceled)
                inputCommmands[State.Jump] = false;
        }

        public void OnWeapon0(InputAction.CallbackContext context)
        {
            weaponType = 0;
        }

        public void OnWeapon1(InputAction.CallbackContext context)
        {
            weaponType = 1;
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
                speedGain = 2;
            else if (context.canceled)
                speedGain = 1;
        }

        public void OnAttack1(InputAction.CallbackContext context)
        {
            if (context.started)
                inputCommmands[State.Attack] = isAttacking == false;
            else if (context.canceled)
                inputCommmands[State.Attack] = false;
        }

        public void OnMouseX(InputAction.CallbackContext context)
        {
            _mouseX = context.ReadValue<float>();
        }

        public void OnMouseY(InputAction.CallbackContext context)
        {
            _mouseY = context.ReadValue<float>();
        }

        public void OnMouseScroll(InputAction.CallbackContext context)
        {
            _mouseScroll = context.ReadValue<float>();
        }
    }
}
