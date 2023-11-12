using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSceneLoad : MonoBehaviour
{
    private bool isCollisioned;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollisioned && collision.CompareTag("Player"))
        {
            LevelManager.Instance.LoadGameStageScene();
            collision.gameObject.SetActive(false);
            isCollisioned = true;
        }
    }
}
