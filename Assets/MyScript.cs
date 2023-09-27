using System.Collections;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    private float interval = 2.0f;

    private void Start()
    {
        // 지정된 시간 간격마다 호출될 함수를 Coroutine으로 실행합니다.
        StartCoroutine(RepeatedMethodWithParameters(interval));
    }

    private IEnumerator RepeatedMethodWithParameters(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // MyMethod에 매개변수를 전달하여 호출합니다.
            int myParameter = 42; // 예제로 정수를 매개변수로 사용
            MyMethod(myParameter);
        }
    }

    private void MyMethod(int parameter)
    {
        // 이곳에 호출할 메서드의 내용을 작성하고 매개변수를 사용합니다.
        Debug.Log("MyMethod 호출됨. 매개변수: " + parameter);
    }
}
