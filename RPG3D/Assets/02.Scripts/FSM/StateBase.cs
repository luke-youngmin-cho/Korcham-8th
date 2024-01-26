using UnityEngine;

namespace RPG.FSM
{
    public abstract class StateBase : IState
    {
        public abstract State state { get; }

        public virtual bool canExecute => true;

        public virtual void OnEnter(Animator animator)
        {
        }

        public virtual void OnExit(Animator animator)
        {
        }

        public virtual State OnUpdate(Animator animator)
        {
            return state;
        }
    }
}
