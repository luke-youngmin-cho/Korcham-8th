using UnityEngine;
using UnityEngine.AI;
using CharacterController = RPG.Controllers.CharacterController;

namespace RPG.AISystems
{
    public class Blackboard
    {
        public Blackboard(BehaviourTree tree)
        {
            this.tree = tree;
            this.transform = tree.transform;
            this.agent = tree.GetComponent<NavMeshAgent>();
            this.controller = tree.GetComponent<CharacterController>();
        }

        // owner
        public BehaviourTree tree;
        public Transform transform;
        public NavMeshAgent agent;
        public CharacterController controller;

        // target
        public Transform target;
    }
}
