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

    private void Awake()
    {
        if (Time.timeScale == 1)
        {
            waitTime = 0.5f;
        }

        waitForSeconds = new WaitForSeconds(waitTime);

        if (!isBlinkType) 
        {
            gameObject.GetComponent<Image>().sprite = unClickedSprite;
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.25f);

        if (isBlinkType)
        {
            StartCoroutine(Blink());
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
