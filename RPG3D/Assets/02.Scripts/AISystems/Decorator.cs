using System;

namespace RPG.AISystems
{
    public class Decorator : Node, IParentOfChild
    {
        public Decorator(BehaviourTree tree, Func<bool> condition) : base(tree)
        {
            this.condition = condition;
        }

        public Node child { get; set; }
        private Func<bool> condition;

        public override Result Invoke()
        {
            if (condition.Invoke())
            {
                return child.Invoke();
            }

            return Result.Failure;
        }
    }
}
