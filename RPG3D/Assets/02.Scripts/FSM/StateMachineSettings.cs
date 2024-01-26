using RPG.Controllers;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace RPG.FSM
{
    public static class StateMachineSettings
    {
        private static Dictionary<State, IState> s_PlayerStates = new Dictionary<State, IState>()
        {
            { State.Move, new Move() },
            { State.Jump, new Jump() },
            { State.Fall, new Fall() },
        };

        public static IDictionary<State, IState> GetPlayerStates()
        {
            return s_PlayerStates;
        }
    }
}
