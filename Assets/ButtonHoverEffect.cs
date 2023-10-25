using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Vector3 originalScale;

    void Start()
    {
        // 버튼 컴포넌트 가져오기
        button = GetComponent<Button>();

        // 원래 버튼 크기 저장
        originalScale = transform.localScale;
    }

    // 커서를 버튼 위에 가져다 댔을 때 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 버튼 크기 확대
        transform.localScale = originalScale * 1.25f; // 크기를 1.1배로 확대하거나 원하는 비율로 조정

        // 원하는 다른 효과 추가 가능
    }

    // 커서가 버튼 위에서 빠져나갈 때 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        // 버튼 크기를 원래 크기로 복구
        transform.localScale = originalScale;

        // 다른 효과를 원래대로 돌리거나 다른 조치를 취할 수 있습니다.
    }
}
