namespace RPG.AISystems
{
    public class Root : Node, IParentOfChild
    {
        public Root(BehaviourTree tree) : base(tree)
        {
        }

        public Node child { get; set; }

        public override Result Invoke()
        {
            tree.stack.Push(child);
            return Result.Success;
        }
    }
}
