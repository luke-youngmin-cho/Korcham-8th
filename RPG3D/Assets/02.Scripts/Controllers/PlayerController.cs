using RPG.FSM;
using UnityEngine;
using System.Collections.Generic;

namespace RPG.Controllers
{
    public class PlayerController : CharacterController
    {
        private StateMachine _machine;

        protected override void Start()
        {
            base.Start();
            _machine = new StateMachine(this, StateMachineSettings.GetPlayerStates());

            _machine.current = State.Move;
        }

        protected override void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            speedGain = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;

            base.Update();
            
            _machine.OnUpdate();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _machine.ChangeState(State.Jump);
            }
        }

        private void LateUpdate()
        {
            _machine.OnLateUpdate();
        }
    }
}
