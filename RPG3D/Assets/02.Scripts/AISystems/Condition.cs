using System;
using UnityEngine;

namespace RPG.AISystems
{
    public class Condition : Node, IParentOfChild
    {
        public Condition(BehaviourTree tree, Func<bool> condition) : base(tree)
        {
            this.condition = condition;
        }

        public Node child { get; set; }
        private Func<bool> condition;

        public override Result Invoke()
        {
            if (condition.Invoke())
            {
                tree.stack.Push(child);
                return Result.Success;
            }

            return Result.Failure;
        }
    }
}
