using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [SerializeField]
    Image[] barImage;
    [SerializeField]
    Sprite barClosed, barOpen;
    [SerializeField]
    Button nextButton, prevButton;

    int currentPage;
    Vector3 targetPos;
    float dragThreshould;

    private void Awake()
    {
        currentPage = 1;
        dragThreshould = Screen.width / 15;
        UpdateBar();
        UpdateArrowButton();
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
        UpdateBar();
        UpdateArrowButton();
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

    private void UpdateBar()
    {
        foreach(var item in barImage)
        {
            item.sprite = barClosed;
        }
        barImage[currentPage - 1].sprite = barOpen;
    }

    private void UpdateArrowButton()
    {
        nextButton.interactable = true;
        prevButton.interactable = true;

        if (currentPage == 1)
        {
            prevButton.interactable = false;
        }
        else if (currentPage == maxPage)
        {
            nextButton.interactable = false;
        }
    }
}
