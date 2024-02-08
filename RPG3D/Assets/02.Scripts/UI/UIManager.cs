using RPG.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.UI
{
    public class UIManager : SingletonBase<UIManager>
    {
        private Dictionary<Type, IUI> _uis = new Dictionary<Type, IUI>();
        private LinkedList<IUI> _popups = new LinkedList<IUI>();
        private List<RaycastResult> _results = new List<RaycastResult>();


        public void Register(IUI ui)
        {
            Type type = ui.GetType();

            if (_uis.ContainsKey(type))
                throw new Exception($"[UIManager] : {type.Name} has already been registered");

            _uis.Add(type, ui);
        }

        public T Get<T>()
        {
            if (_uis.TryGetValue(typeof(T), out IUI ui))
                return (T)ui;

            return default(T);
        }

        public void Push(IUI ui)
        {
            int sortingOrder = 1;
            // PopUp 이 하나라도 되어있다면
            if (_popups.Last?.Value != null)
            {
                _popups.Last.Value.inputActionEnable = false;
                sortingOrder = _popups.Last.Value.sortingOrder + 1;
            }

            ui.inputActionEnable = true;
            ui.sortingOrder = sortingOrder;
            _popups.Remove(ui);
            _popups.AddLast(ui);

            if (_popups.Count == 1)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        public void Pop(IUI ui)
        {
            // 빼려는 UI 가 마지막거면 바로 앞의 UI 입력동작 활성화
            if (_popups.Count >= 2 &&
                _popups.Last.Value == ui)
                _popups.Last.Previous.Value.inputActionEnable = true;

            _popups.Remove(ui);

            if (_popups.Count == 0)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        /// <summary>
        /// 현재 핸들링중인 UI 말고 다른 UI 가 선택됐는지 확인
        /// </summary>
        /// <param name="ui"> 원래 쓰던거 (가장 최상단에 있던거) </param>
        /// <param name="other"> 선택된 그 아래 다른거 </param>
        /// <returns></returns>
        public bool TryCastOther(IUI ui, out IUI other)
        {
            var node = _popups.Last;

            while (node != null)
            {
                if (node.Value != ui)
                {
                    _results.Clear();
                    node.Value.Raycast(_results);
                    if (_results.Count > 0)
                    {
                        other = _results[0].gameObject.transform.root.GetComponent<IUI>();
                        return true;
                    }
                }
                
                node = node.Previous;
            }

            other = null;
            return false;
        }
    }
}
