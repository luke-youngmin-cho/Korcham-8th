using UnityEngine;
using RPG.Animations;
using System.Collections.Generic;

namespace RPG.Controllers
{
    public class PlayerController : CharacterController
    {
        public GameObject twoHandedSword;

        protected override void Start()
        {
            base.Start();
            aiOn = false;
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

            inputCommmands[State.Jump] = Input.GetKeyDown(KeyCode.Space);
            inputCommmands[State.Attack] = isAttacking == false &&
                                           Input.GetMouseButton(0);

            base.Update();
        }
    }
}
