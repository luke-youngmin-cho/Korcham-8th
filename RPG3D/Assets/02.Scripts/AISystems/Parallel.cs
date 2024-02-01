namespace RPG.AISystems
{
    public class Parallel : Composite
    {
        public Parallel(BehaviourTree tree, Policy successPolicy) : base(tree)
        {
            _successPolicy = successPolicy;
        }


        public enum Policy
        {
            RequireOne,
            RequireAll,
        }
        private Policy _successPolicy;
        private int _successCount;
        

        public override Result Invoke()
        {
            Result result = Result.Failure;

            for (int i = currentChildIndex; i < children.Count; i++)
            {
                result = children[i].Invoke();

                switch (result)
                {
                    case Result.Failure:
                        break;
                    case Result.Success:
                        _successCount++;
                        break;
                    case Result.Running:
                        return result;
                    default:
                        break;
                }
                currentChildIndex++;
            }

            // 성공 정책에 따라 결과 산출
            switch (_successPolicy)
            {
                case Policy.RequireOne:
                    result = _successCount > 0 ? Result.Success : Result.Failure;
                    break;
                case Policy.RequireAll:
                    result = _successCount == children.Count ? Result.Success : Result.Failure;
                    break;
                default:
                    throw new System.Exception();
            }

            currentChildIndex = 0;
            _successCount = 0;
            return result;
        }
    }
}
