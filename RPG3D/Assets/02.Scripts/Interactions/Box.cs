using RPG.Interactions;
using System;
using UnityEngine;

public class Box : MonoBehaviour, IHp, IInteractable
{
    [SerializeField] LayerMask _notifyTargetMask;

    public float hpValue
    {
        get
        {
            return _hp;
        }
        set
        {
            value = Mathf.Clamp(value, 0, _hpMax);

            if (_hp == value)
                return;

            _hp = value;

            //onHpChanged(value);
            //onHpChanged.Invoke(value);
            onHpChanged?.Invoke(value); // null check 연산자 : 왼쪽 피연산자가 null 일경우 null 반환

            if (value <= 0)
                onHpMin?.Invoke();
            else if (value >= _hpMax)
                onHpMax?.Invoke();
        }
    }

    public float hpMax => _hpMax;
    float _hp;
    float _hpMax = 4;
    public event Action<float> onHpChanged;
    public event Action<float> onHpDepleted;
    public event Action<float> onHpRecovered;
    public event Action onHpMin;
    public event Action onHpMax;

    private void Start()
    {
        onHpMin += () => Destroy(gameObject);
    }

    public void DepleteHp(float amount)
    {
        if (amount <= 0)
            return;

        amount = 1;
        hpValue -= amount;
        onHpDepleted?.Invoke(amount);
    }

    public void RecoverHp(float amount)
    {
        hpValue += amount;
        onHpRecovered?.Invoke(amount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer & _notifyTargetMask) > 0)
        {
            Debug.Log("This is a box.");
        }
    }

    public void Interaction(Interactor interactor)
    {
        Debug.Log($"[Box] : 상호작용 시작");
    }
}
