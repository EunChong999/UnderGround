using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField]
    private float scaleDuration;
    [SerializeField]
    private Ease easeType;

    protected Button button;
    Vector3 upScale = new Vector3(1.2f, 1.2f, 1);

    protected void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Anim);
    }

    protected void Anim()
    {
        GetComponent<RectTransform>().DOScale(upScale, scaleDuration).SetEase(easeType).SetUpdate(true);
        GetComponent<RectTransform>().DOScale(Vector3.one, scaleDuration).SetDelay(scaleDuration).SetUpdate(true);
    }
}
