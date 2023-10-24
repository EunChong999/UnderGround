using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField]
    int maxPage;
    [SerializeField]
    Vector3 pageStep;
    [SerializeField]
    RectTransform levelPagesRect;
    [SerializeField]
    float tweenDuration;
    [SerializeField]
    Ease easeType;

    int currentPage;
    Vector3 targetPos;
    float dragThreshould;

    private void Awake()
    {
        currentPage = 1;
        dragThreshould = Screen.width / 15;
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos.x += pageStep.x;
            MovePage();
        }
    }

    public void Prev()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos.x -= pageStep.x;
            MovePage();
        }
    }

    private void MovePage()
    {
        levelPagesRect.DOAnchorPosX(targetPos.x, tweenDuration).SetEase(easeType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x)
            {
                Prev();
            }
            else
            {
                Next();
            }
        }
        else
        {
            MovePage();
        }
    }
}
