using UnityEngine;
using System;
using System.Collections;

public class CoroutineExample : MonoBehaviour
{
    private bool condition = false;
    private int someValue = 42; // ������ ���� ���� �����ϴ� ����

    private void Start()
    {
        // �ڷ�ƾ �Լ��� ȣ���մϴ�.
        StartCoroutine(CheckCondition());
    }

    private IEnumerator CheckCondition()
    {
        // ���� �ð�(��: 2��) ����մϴ�.
        yield return new WaitForSeconds(2f);

        // Ŭ���� ��� �����κ��� ���� ���ͼ� ������ Ȯ���մϴ�.
        bool result = CheckSomeCondition(someValue);

        // ����� Ŭ���� ��� ������ �����մϴ�.
        condition = result;

        // ����� ����մϴ�.
        Debug.Log("Condition is: " + condition);
    }

    private bool CheckSomeCondition(int value)
    {
        // ���⿡�� �Ű������� ����� ���� Ȯ�� ������ �ۼ��մϴ�.
        // ���� ���, value�� Ư�� ������ �����ϴ��� Ȯ���ϴ� ������ �ۼ��մϴ�.
        return value == 42; // ������ value�� 42�� ��쿡 true�� ��ȯ�ϵ��� ����
    }
}
