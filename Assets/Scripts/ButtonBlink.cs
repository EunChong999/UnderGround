using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlink : MonoBehaviour
{
    public bool isBlinkType;
    [SerializeField]
    private Sprite unClickedSprite;
    [SerializeField]
    private Sprite onClickedSprite;
    [SerializeField]
    private float waitTime;

    private WaitForSeconds waitForSeconds;
    private bool isBlinking;

    private void OnEnable()
    {
        isBlinking = false;
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            waitTime = 0.5f;
        }
        else
        {
            waitTime = 0.05f;
        }

        waitForSeconds = new WaitForSeconds(waitTime);

        if (!isBlinkType)
        {
            gameObject.GetComponent<Image>().sprite = unClickedSprite;
        }

        if (isBlinkType && !isBlinking)
        {
            StartCoroutine(Blink());
            isBlinking = true;
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            if (gameObject.GetComponent<Image>().sprite == unClickedSprite)
            {
                gameObject.GetComponent<Image>().sprite = onClickedSprite;
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = unClickedSprite;
            }

            yield return waitForSeconds;
        }
    }
}
