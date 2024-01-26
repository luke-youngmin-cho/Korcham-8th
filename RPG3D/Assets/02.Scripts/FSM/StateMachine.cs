using System.Collections.Generic;
using UnityEngine;
using CharacterController = RPG.Controllers.CharacterController;

namespace RPG.FSM
{
    internal class StateMachine
    {
        public StateMachine(CharacterController owner, IDictionary<State, IState> states)
        {
            this.owner = owner;
            this.states = new Dictionary<State, IState>(states);
            _animator = owner.GetComponent<Animator>();
        }


        public CharacterController owner;
        public State current;
        private Dictionary<State, IState> states; // 상태에 맞는 상호작용 객체를 검색할수 있는 사전
        private Animator _animator;
        private bool _isDirty;

        public void OnUpdate()
        {
            IState stateInterface = states[current]; // 현재 상태에 맞는 기능
            State next = // 다음에는 어떤 상태로 바꿔야 하는지
                stateInterface.OnUpdate(_animator); // 그 기능들 중에서 매프레임마다 실행해야하는 기능 호출
            ChangeState(next); // 다음 상태로 전환.

            // 위에꺼 줄여쓰면 :
            //ChangeState(states[current].OnUpdate(_animator));
        }

        public void OnLateUpdate()
        {
            _animator.SetBool("isDirty", false);
            _isDirty = false;
        }

        public bool ChangeState(State newState)
        {
            // 아직 갱신되지 못한 상태가 있으면 다른상태 전환 안되게
            if (_isDirty)
                return false;

            // 이미 해당 상태면 바꿀필요 없음
            if (newState == current)
                return false;

            // 바꾸려는 상태 실행 가능한지 ?
            if (states[newState].canExecute == false)
                return false;

            states[current].OnExit(_animator); // 기존 상태에서 빠져나오는 기능 호출
            _animator.SetInteger("state", (int)newState);
            _animator.SetBool("isDirty", true);
            _isDirty = true;
            states[newState].OnEnter(_animator); // 다음 상태 진입하는 기능 호출
            current = newState; // 상태 정상 갱신
            return true;
        }
    }
}
