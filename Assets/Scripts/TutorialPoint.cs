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
    private GameObject WASD;

    [HideInInspector]
    public bool isPointCleared;

    private void Update()
    {
        if (isPointCleared)
        {
            guideUI.SetActive(false);
            guideUI = WASD;
            guideUI.SetActive(true);
            isPointCleared = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (guideUI.name == "WASD")
            {
                collision.GetComponent<UndergroundMovement>().isLockedKey[2] = false;
                collision.GetComponent<BombSpawner>().isLockedKey[2] = true;
            }
            else if (guideUI.name == "Arrow")
            {
                collision.GetComponent<UndergroundMovement>().isLockedKey[2] = true;
                collision.GetComponent<BombSpawner>().isLockedKey[2] = false;
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
            collision.GetComponent<UndergroundMovement>().isLockedKey[2] = true;
            collision.GetComponent<BombSpawner>().isLockedKey[2] = true;

            WASD.SetActive(false);
            guideUI.SetActive(false);
        }
    }
}
