using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointCollisionCheck : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Debug.Log("Here");
            enemy.GetComponent<BaseEnemyMovement>().OnPlayerSignal();
        }
    }
}
