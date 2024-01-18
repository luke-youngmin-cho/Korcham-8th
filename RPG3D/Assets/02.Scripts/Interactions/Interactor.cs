using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Interactions
{
    // RequireComponent : Editor 상에서 특정 컴포넌트를 주입하는 특성
    [RequireComponent(typeof(CapsuleCollider))]
    public class Interactor : MonoBehaviour
    {
        private IInteractable _interacting;
        private Vector3 _p0Rel, _p1Rel;
        private float _radius;
        [SerializeField] private float _detectRangeGain = 2.0f;

        private void Awake()
        {
            CapsuleCollider col = GetComponent<CapsuleCollider>();
            _p0Rel = col.center + (- col.height / 2.0f + col.radius) * Vector3.up;
            _p1Rel = col.center + (+ col.height / 2.0f - col.radius) * Vector3.up;
            _radius = col.radius * _detectRangeGain;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                IInteractable interactable = DetectInteractable();
                if (TryInteraction(interactable))
                {
                    _interacting = interactable;
                }
            }
        }

        private IInteractable DetectInteractable()
        {
            Collider[] cols =
                Physics.OverlapCapsule(transform.position + _p0Rel,
                                       transform.position + _p1Rel,
                                       _radius);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].TryGetComponent(out IInteractable interactable))
                {
                    return interactable;
                }
            }
            return null;
        }

        private bool TryInteraction(IInteractable interactable)
        {
            if (interactable != null)
            {
                interactable.Interaction(this);
                return true;
            }

            return false;
        }
    }
}