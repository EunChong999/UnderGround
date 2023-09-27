using System.Collections;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    private float interval = 2.0f;

    private void Start()
    {
        // ������ �ð� ���ݸ��� ȣ��� �Լ��� Coroutine���� �����մϴ�.
        StartCoroutine(RepeatedMethodWithParameters(interval));
    }

    private IEnumerator RepeatedMethodWithParameters(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // MyMethod�� �Ű������� �����Ͽ� ȣ���մϴ�.
            int myParameter = 42; // ������ ������ �Ű������� ���
            MyMethod(myParameter);
        }
    }

    private void MyMethod(int parameter)
    {
        // �̰��� ȣ���� �޼����� ������ �ۼ��ϰ� �Ű������� ����մϴ�.
        Debug.Log("MyMethod ȣ���. �Ű�����: " + parameter);
    }
}
