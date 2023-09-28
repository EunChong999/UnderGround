using System.Collections;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    [SerializeField] private float interval;

    private void Start()
    {
        // ������ �ð� ���ݸ��� ȣ��� �Լ��� Coroutine���� �����մϴ�.
        StartCoroutine(RepeatedMethodWithParameters(interval));
    }

    private IEnumerator RepeatedMethodWithParameters(float interval)
    {
        for(int i = 0; i < 10;  i++) 
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
