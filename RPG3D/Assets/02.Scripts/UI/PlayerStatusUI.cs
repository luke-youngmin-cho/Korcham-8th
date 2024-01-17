using UnityEngine;
using UnityEngine.UI;
using RPG.Controllers;
using CharacterController = RPG.Controllers.CharacterController;
using System;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] Slider _hpBar;
    [SerializeField] CharacterController _controller;

    private void Start()
    {
        SetHpBar(_controller.hpMax);
        _controller.onHpChanged += RefreshHpBar;
        _controller.onHpChanged += (hp) => _hpBar.value = hp;
        _controller.onHpChanged += (float hp) =>
        {
            _hpBar.value = hp;
        };
    }

    public void SetHpBar(float hpMax)
    {
        _hpBar.minValue = 0;
        _hpBar.maxValue = hpMax;
        _hpBar.value = hpMax;
    }

    // 함수 오버헤드 : 함수를호출하는데 드는 비용이 함수 연산내용보다 클때
    // 인라인 함수 : 함수 구현부를 해당 라인에 직접 삽입하는 함수
    // 람다식(익명함수) : C# 에서 인라인함수를 구현할 때 쓰는 표현식, 컴파일러가 알아서 판단할 수 있는 요소들을 모두 생략한 식
    public void RefreshHpBar(float hp)
    {
        _hpBar.value = hp;
    }

    public delegate float SomethingHandler(int param1, ulong param2);
    SomethingHandler onSomethingHappened;

    // Action 대리자 : void 를 반환하고 파라미터 0~n 개까지 받는 제네릭형식의 대리자를 정의하고있음.
    public Action action1;
    public Action<int> action2;
    // Func 대리자 : Generic type을 반환하고 파라미터 0~n 개까지 받는 제네릭 형식의 대리자를 정의하고있음.
    public Func<int> func1;
    // Predicate 대리자 : bool 을 반환하고 파라미터 1개 받는 제네릭 형식의 대리자를 정의하고있음.
    // 주로 특정 아이템의 매치조건 을 확인할때 사용
    public Predicate<int> predicate1;

    public void Test()
    {
        onSomethingHappened = Something;
        onSomethingHappened = (param1, param2) =>
        {
            float result = (float)param1 + param2;
            return result;
        };// 람다식으로 써주세요
        onSomethingHappened = (param1, param2) => (float)param1 + param2;
    }

    public float Something(int param1, ulong param2)
    {
        float result = (float)param1 + param2;
        return result;
    }
}

