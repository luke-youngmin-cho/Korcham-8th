using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace RPG.UI
{
    public interface IUI
    {
        int sortingOrder { get; set; }
        bool inputActionEnable { get; set; }

        void InputAction();
        void Show();
        void Hide();
        void Raycast(List<RaycastResult> results);
    }
}