using System.Collections;
using System.Collections.Generic;

namespace RPG.AISystems
{
    public class BehaviourTree
    {
        public Blackboard blackboard;
        public Stack<Node> stack;
        public Root root;

        IEnumerator C_Tick()
        {
            stack.Clear();
            stack.Push(root);

            while (stack.Count > 0)
            {
                Node current = stack.Pop();
                Result result = current.Invoke();

                if (result == Result.Running)
                {
                    stack.Push(current);
                    yield return null;
                }
            }
        }
    }
}