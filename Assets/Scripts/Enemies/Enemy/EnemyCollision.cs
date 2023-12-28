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
            Debug.Log("»Æ¿Œ");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && LayerMask.LayerToName(collision.gameObject.layer) != "Enemy" &&
            !enemyHealth.isDead)
        {
            GetComponent<AudioManager>().Play("Damage");
            enemyHealth.Dead();
        }
    }
}
