using UnityEngine;
using RPG.Animations;
using System.Collections.Generic;

namespace RPG.Controllers
{
    public class PlayerController : CharacterController
    {
        protected override void Start()
        {
            base.Start();

            inputCommmands = new Dictionary<State, bool>()
            {
                { State.Jump, false},
            };
        }

        protected override void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            speedGain = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;

            inputCommmands[State.Jump] = Input.GetKeyDown(KeyCode.Space);
            inputCommmands[State.Attack] = isAttacking == false &&
                                           Input.GetMouseButtonDown(0);

            base.Update();
        }
    }
}
