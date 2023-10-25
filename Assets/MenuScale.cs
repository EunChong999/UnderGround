using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuScale : MonoBehaviour
{
    [SerializeField]
    private Transform trans;
    [SerializeField]
    private float duration1;
    [SerializeField]
    private float duration2;
    [SerializeField]
    private float delayTime;
    [SerializeField]
    private float scaleValue1;
    [SerializeField]
    private float scaleValue2;
    [SerializeField]
    private Ease easeType1;
    [SerializeField]
    private Ease easeType2;
    [SerializeField]
    private bool isSinType;

    private Vector3 originalScale;
    private Vector3 scaleTo1;
    private Vector3 scaleTo2;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTime);  
        trans.gameObject.SetActive(true);
        originalScale = trans.localScale;
        scaleTo1 = originalScale * scaleValue1;
        scaleTo2 = originalScale * scaleValue2;

        trans.DOScale(scaleTo1, duration1)
            .SetEase(easeType1);

        trans.DOScale(originalScale, duration1)
            .SetEase(easeType1);

        yield return new WaitForSeconds(0.5f);

        if (isSinType)
        {
            trans.DOScale(scaleTo2, duration2)
                .SetEase(easeType2)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
