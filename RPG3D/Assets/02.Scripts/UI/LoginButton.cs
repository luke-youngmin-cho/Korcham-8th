using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            UIManager.instance
                .Get<ConfirmWindow>()
                .Show("Check ID & Password again.", null);
        });
    }
}
