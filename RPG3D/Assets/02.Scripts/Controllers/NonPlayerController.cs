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

        [Header("Behaviours")]
        [SerializeField] float _seekRadius = 5.0f;
        [SerializeField] float _seekHeight = 1.0f;
        [SerializeField] float _seekAngle = 90.0f;
        [SerializeField] LayerMask _seekTargetMask;
        [SerializeField] float _seekMaxDistance = 10.0f;
        [SerializeField] float _attackRange = 1.0f;

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
                    .Seek(_seekRadius, _seekHeight, _seekAngle, _seekTargetMask, _seekMaxDistance)
                    .Decorator(() => Vector3.Distance(transform.position, tree.blackboard.target.position) <= _attackRange)
                        .Execution(() =>
                        {
                            Debug.Log("Attack!!");
                            return Result.Success;
                        });

            aiOn = true;
        }
    }
}