namespace RPG.AISystems
{
    public class Sequence : Composite
    {
        public Sequence(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Invoke()
        {
            Result result = Result.Success;

            for (int i = currentChildIndex; i < children.Count; i++)
            {
                result = children[i].Invoke();

                switch (result)
                {
                    case Result.Failure:
                        {
                            currentChildIndex = 0;
                            return result;
                        }
                    case Result.Success:
                        {
                            currentChildIndex++;
                        }
                        break;
                    case Result.Running:
                        {
                            return result;
                        }
                    default:
                        break;
                }
            }

            currentChildIndex = 0;
            return result;
        }
    }
}
