using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad dontDestroyOnLoad; // DonDestroyOnLoad Ŭ���� ����

    private void Awake() // ���� ���۵� ������ 
    {
        DontDestroyOnLoad(this); // �ش� ������Ʈ�� �� ��ȯ�ÿ��� �ı����� �ʵ��� �Ѵ�.

        if (dontDestroyOnLoad == null) // donDestroyOnLoad�� ���� �ʱ�ȭ���� �ʾҴٸ�
        {
            dontDestroyOnLoad = this; // ���� �ν��Ͻ��� donDestroyOnLoad�� �Ҵ��Ͽ� ���� �������� �����ǵ��� �Ѵ�.
        }
        else // donDestroyOnLoad�� �ʱ�ȭ�Ǿ��ٸ�
        {
            Destroy(gameObject); // �ش� ������Ʈ�� �ı��Ͽ�, ���� �Ѿ ������ ������Ʈ�� �ߺ����� �����Ǵ� ���� �����Ѵ�.
        }
    }
}


