using UnityEngine;
using System;
using System.Collections;

public class CoroutineExample : MonoBehaviour
{
    private bool condition = false;
    private int someValue = 42; // 예제로 정수 값을 전달하는 변수

    private void Start()
    {
        // 코루틴 함수를 호출합니다.
        StartCoroutine(CheckCondition());
    }

    private IEnumerator CheckCondition()
    {
        // 일정 시간(예: 2초) 대기합니다.
        yield return new WaitForSeconds(2f);

        // 클래스 멤버 변수로부터 값을 얻어와서 조건을 확인합니다.
        bool result = CheckSomeCondition(someValue);

        // 결과를 클래스 멤버 변수에 저장합니다.
        condition = result;

        // 결과를 출력합니다.
        Debug.Log("Condition is: " + condition);
    }

    private bool CheckSomeCondition(int value)
    {
        // 여기에서 매개변수를 사용한 조건 확인 로직을 작성합니다.
        // 예를 들어, value가 특정 조건을 만족하는지 확인하는 로직을 작성합니다.
        return value == 42; // 예제로 value가 42인 경우에 true를 반환하도록 설정
    }
}
