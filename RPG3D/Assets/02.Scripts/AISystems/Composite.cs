using System.Collections.Generic;

namespace RPG.AISystems
{
    public abstract class Composite : Node, IParentOfChildren
    {
        protected Composite(BehaviourTree tree) : base(tree)
        {
            children = new List<Node>();
        }

        public List<Node> children { get; set; }

        // 마지막으로 실행했던 자식의 인덱스.
        // 자식이 Running 을 반환했다가 빠져나올경우,
        // 이 Composite 는 다음 자식을 이어서 탐색해야하기 때문에
        // 자식 탐색 할때마다 인덱스를 기억해두어야함.
        protected int currentChildIndex;
    }
}
