using System;

internal interface IHp
{
    float hpValue { get; set; }
    float hpMax { get; }
    event Action<float> onHpChanged; // 변화된 후의 값
    event Action<float> onHpDepleted; // 고갈된 양
    event Action<float> onHpRecovered; // 회복된 양
    event Action onHpMin;
    event Action onHpMax;
    void DepleteHp(float amount);
    void RecoverHp(float amount);
}
