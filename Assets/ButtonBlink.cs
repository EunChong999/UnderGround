using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlink : MonoBehaviour
{
    [SerializeField]
    private Sprite unClickedSprite;
    [SerializeField]
    private Sprite onClickedSprite;
    [SerializeField]
    private float waitTime;

    private WaitForSeconds waitForSeconds;

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(waitTime);
    }

    private void OnEnable()
    {
        StartCoroutine(Blink());
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
