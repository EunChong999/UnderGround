using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialPoint : MonoBehaviour
{
    [SerializeField]
    private bool[] direction;

    [SerializeField]
    private GameObject guideUI;

    [SerializeField]
    private bool isStartPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!isStartPoint)
            {
                Time.timeScale = 0.1f;
            }

            guideUI.SetActive(true);

            for (int i = 0; i < direction.Length; i++)
            {
                if (direction[i])
                {
                    guideUI.transform.GetChild(i).GetComponent<ButtonBlink>().isBlinkType = true;
                }
                else
                {
                    guideUI.transform.GetChild(i).GetComponent<ButtonBlink>().isBlinkType = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 1f;
            guideUI.SetActive(false);
        }
    }
}
