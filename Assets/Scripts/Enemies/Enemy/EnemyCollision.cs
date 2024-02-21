using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    BasicEnemy enemyHealth;

    private void Start()
    {
        enemyHealth = GetComponent<BasicEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && LayerMask.LayerToName(collision.gameObject.layer) != "Enemy")
        {
            GetComponent<AudioManager>().Play("Damage");
            Invoke(nameof(Damage), 0.1f);
        }
    }

    private void Damage()
    {
        if (!enemyHealth.isDead)
        {
            enemyHealth.Dead();
        }
    }
}
