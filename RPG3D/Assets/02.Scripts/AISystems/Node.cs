namespace RPG.AISystems
{
    public abstract class Node
    {
        public Node(BehaviourTree tree)
        {
            this.tree = tree;
            this.blackboard = tree.blackboard;
        }


        protected BehaviourTree tree;
        protected Blackboard blackboard;

        public abstract Result Invoke();
    }
}
