using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class MiniHpBarUI : MonoBehaviour
    {
        private Slider _hp;


        private void Start()
        {
            _hp = GetComponentInChildren<Slider>(); // 자식중에서 Slider 탐색
            IHp hpInterface = GetComponentInParent<IHp>(); // 부모에서 인터페이스 탐색
            _hp.minValue = 0.0f;
            _hp.maxValue = hpInterface.hpMax;
            _hp.value = hpInterface.hpValue;
            hpInterface.onHpChanged += (value) => _hp.value = value;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + Camera.main.transform.forward,
                             Camera.main.transform.up);
        }
    }
}