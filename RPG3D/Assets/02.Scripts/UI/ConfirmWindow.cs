using FUnit.GameObjectExtensions;
using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.UI
{
    public class ConfirmWindow : UIBase
    {
        private TMP_Text _message;
        private Button _confirm;


        public void Show(string message, UnityAction onConfirm)
        {
            base.Show();
            _message.text = message;
            _confirm.onClick.RemoveAllListeners();

            if (onConfirm != null)
                _confirm.onClick.AddListener(onConfirm);

            _confirm.onClick.AddListener(Hide);
        }


        protected override void Awake()
        {
            base.Awake();
            _message = transform.Find("Panel").Find("Text - Message").GetComponent<TMP_Text>();
            _confirm = transform.Find("Panel").Find("Button - Confirm").GetComponent<Button>();
        }
    }
}