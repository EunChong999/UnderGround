using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    EnemyHealth enemyHealth;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && LayerMask.LayerToName(collision.gameObject.layer) != "Enemy" &&
            !enemyHealth.isDead)
        {
            enemyHealth.Dead();
        }
    }
}
