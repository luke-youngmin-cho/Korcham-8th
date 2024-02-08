using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.UI
{
    [RequireComponent(typeof(Canvas))]
    public class UIBase : MonoBehaviour, IUI
    {
        public int sortingOrder 
        { 
            get => canvas.sortingOrder;
            set => canvas.sortingOrder = value;
        }

        public bool inputActionEnable { get; set; }
        protected Canvas canvas;
        private GraphicRaycaster _module;

        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
            _module = GetComponent<GraphicRaycaster>();
            UIManager.instance.Register(this);
        }


        public virtual void Show()
        {
            UIManager.instance.Push(this);
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            UIManager.instance.Pop(this);
            gameObject.SetActive(false);
        }

        public virtual void InputAction()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (UIManager.instance.TryCastOther(this, out IUI other))
                {
                    other.Show();
                }
            }
        }

        public void Raycast(List<RaycastResult> results)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            _module.Raycast(pointerEventData, results);
        }
    }
}