using UnityEngine;

namespace RPG.Controllers
{
    public class PlayerController : CharacterController
    {
        protected override void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            base.Update();
        }
    }
}
