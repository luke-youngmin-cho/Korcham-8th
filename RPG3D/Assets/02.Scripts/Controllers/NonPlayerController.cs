using UnityEngine;

namespace RPG.Controllers
{
    public class NonPlayerController : CharacterController
    {
        [SerializeField] Transform _agentTarget;

        protected override void Start()
        {
            base.Start();
            aiOn = true;

        }

        protected override void Update()
        {
            base.Update();
            agent.SetDestination(_agentTarget.position);
        }
    }
}