using UnityEngine;

namespace RPG.FSM
{
    public interface IState
    {
        State state { get; }
        bool canExecute { get; }
        void OnEnter(Animator animator);
        State OnUpdate(Animator animator);
        void OnExit(Animator animator);
    }
}
