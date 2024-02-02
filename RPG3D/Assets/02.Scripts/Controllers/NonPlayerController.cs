using RPG.AISystems;
using System.Collections.Generic;
using UnityEngine;
using RPG.Animations;

namespace RPG.Controllers
{
    [RequireComponent(typeof(BehaviourTree))]
    public class NonPlayerController : CharacterController
    {
        [SerializeField] Transform _agentTarget;

        [Header("AI Behaviours")]
        [Header("Seek")]
        [SerializeField] float _seekRadius = 5.0f;
        [SerializeField] float _seekHeight = 1.0f;
        [SerializeField] float _seekAngle = 90.0f;
        [SerializeField] LayerMask _seekTargetMask;
        [SerializeField] float _seekMaxDistance = 10.0f;

        [Header("Attack")]
        [SerializeField] float _attackRange = 1.0f;

        [Header("Patrol")]
        [SerializeField] float _patrolRadius = 5.0f;
        [SerializeField] float _patrolIdleTime = 2.0f;
        [Range(0f, 1f)]
        [SerializeField] float _patrolIdleRatio = 0.5f;

        protected override void Start()
        {
            base.Start();
            inputCommmands = new Dictionary<State, bool>()
            {
                { State.Jump, false },
                { State.Attack, false }
            };

            BehaviourTree tree = GetComponent<BehaviourTree>();

            // -- Before chaining --
            //tree.root = new Root(tree);
            //tree.root.child = new Sequence(tree);
            //((Sequence)tree.root.child).children.Add(new Execution(tree, () => Result.Success));
            //((Sequence)tree.root.child).children.Add(new Decorator(tree, () => true));
            //((Decorator)((Sequence)tree.root.child).children[1]).child = new Execution(tree, () => Result.Success);


            // -- After chaining --
            tree.StartBuild()
                .Sequence()
                    .Patrol(_patrolRadius, _patrolIdleTime, _patrolIdleRatio)
                    .Seek(_seekRadius, _seekHeight, _seekAngle, _seekTargetMask, _seekMaxDistance)
                    .Condition(() => Vector3.Distance(transform.position, tree.blackboard.target.position) <= _attackRange)
                        .Attack(_attackRange);

            aiOn = true;
        }
    }
}