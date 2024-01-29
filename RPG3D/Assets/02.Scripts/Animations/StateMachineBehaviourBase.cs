using System;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = RPG.Controllers.CharacterController;

namespace RPG.Animations
{
    public class StateMachineBehaviourBase : StateMachineBehaviour
    {
        public Dictionary<State, Func<Animator, bool>> transitions;
        protected CharacterController controller;

        public virtual void Init(CharacterController controller)
        {
            this.controller = controller;
        }


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.SetBool("isDirty", false);
        }


        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            foreach (var transition in transitions)
            {
                ChangeState(animator, transition.Key);
            }
        }

        protected bool ChangeState(Animator animator, State newState)
        {
            if (transitions.ContainsKey(newState) == false)
                return false;

            if (transitions[newState].Invoke(animator) == false)
                return false;

            animator.SetInteger("state", (int)newState);
            animator.SetBool("isDirty", true);
            return true;
        }
    }
}