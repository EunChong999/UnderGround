using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Vector3 originalScale;

    void Start()
    {
        // ��ư ������Ʈ ��������
        button = GetComponent<Button>();

        // ���� ��ư ũ�� ����
        originalScale = transform.localScale;
    }

    // Ŀ���� ��ư ���� ������ ���� �� ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ư ũ�� Ȯ��
        transform.localScale = originalScale * 1.25f; // ũ�⸦ 1.1��� Ȯ���ϰų� ���ϴ� ������ ����

        // ���ϴ� �ٸ� ȿ�� �߰� ����
    }

    // Ŀ���� ��ư ������ �������� �� ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        // ��ư ũ�⸦ ���� ũ��� ����
        transform.localScale = originalScale;

        // �ٸ� ȿ���� ������� �����ų� �ٸ� ��ġ�� ���� �� �ֽ��ϴ�.
    }
}
