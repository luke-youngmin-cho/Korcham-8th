using System.Collections;
using UnityEngine;

public class DummyProgram
{
    void Main()
    {
        Test test = new Test(); // 이렇게쓰믄 안댐
        new GameObject().AddComponent<Test>(); // 요렇게 써야함
    }
}

public class Test : MonoBehaviour
{
    // 플레이모드중 스크립트 인스턴스가 생성될 때 호출됨. 
    // Hierarchy 에서 GameObject 가 활성화되어있으면서 이 컴포넌트가 생성될때 호출
    // 생성자 내용 구현 대용
    private void Awake()
    {
        Debug.Log("[Test] : Awake");
    }

    // 이 스크립트 인스턴스가 활성화 될때마다 호출
    // 이 스크립트 인스턴스가 활성화 되어있으면서 이 스크립트인스턴스를 컴포넌트로 가지는 GameObject 가 활성화 될 때마다 호출
    private void OnEnable()
    {
        Debug.Log("[Test] : OnEnable");
    }

    private void Reset()
    {
        Debug.Log("[Test] : Reset");
    }

    // Update() 호출직전에 한번만 호출됨
    void Start()
    {
        StartCoroutine(C_Init());
    }

    // 매 프레임마다 호출
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { }
    }

    // Update 프레임의 모든 연산결과를 토대로 마지막에 연산해야 하는 내용을 작성
    private void LateUpdate()
    {
        
    }

    // 매 고정프레임마다 호출
    private void FixedUpdate()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("[Test] : Mouse down");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Click me"))
        {
            Debug.Log($"[Test] : Clicked test button.");
        }
    }

    IEnumerator C_Init()
    {
        yield return new WaitForEndOfFrame();
    }

    // 이 스크립트 인스턴스가 비활성화 될때마다 호출
    // 이 스크립트 인스턴스가 활성화 되어있으면서 이 스크립트인스턴스를 컴포넌트로 가지는 GameObject 가 비활성화 될 때마다 호출
    private void OnDisable()
    {
        Debug.Log("[Test] : OnDisable");
    }

    // 이 컴포넌트가 파괴될때 호출
    private void OnDestroy()
    {
        Debug.Log("[Test] : Destroyed");
    }
}
