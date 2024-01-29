using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace RPG.Animations
{
    public class Attack : StateMachineBehaviourBase
    {
        [SerializeField] private List<int> _comboStackLimits;
        private int _attackComboStack;
        private float _comboStackResetTime = 0.5f;


        public override void Init(Controllers.CharacterController controller)
        {
            base.Init(controller);

            transitions = new Dictionary<State, System.Func<Animator, bool>>()
            {
                {
                    State.Attack, (animator) =>
                    {
                        if (controller.inputCommmands[State.Attack] == false)
                            return false;
                        
                        return true;
                    }
                },
            };
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            controller.isAttacking = true;
            animator.SetInteger("attackComboStack", _attackComboStack);
            _attackComboStack = _attackComboStack < _comboStackLimits[controller.weaponType] ?
                                _attackComboStack + 1 : 0;
            Debug.Log($"[Attack] : {_attackComboStack}, enter");

            if (controller.comboStackResetCoroutine != null)
                controller.StopCoroutine(controller.comboStackResetCoroutine);

            controller.comboStackResetCoroutine 
                = controller.StartCoroutine(C_ResetCombo(animator, stateInfo.length));
        }


        IEnumerator C_ResetCombo(Animator animator, float clipLength)
        {
            yield return new WaitForSeconds(clipLength + _comboStackResetTime);

            controller.isAttacking = false;
            _attackComboStack = 0;
            animator.SetInteger("attackComboStack", _attackComboStack);
        }
    }
}
