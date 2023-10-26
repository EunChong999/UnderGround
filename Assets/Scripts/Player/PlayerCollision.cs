using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerHealth playerHealth;
    UndergroundMovement undergroundMovement;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        undergroundMovement = GetComponent<UndergroundMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!LevelManager.Instance.isLoading)
        {
            if (collision.CompareTag("Obstacle") &&
                !playerHealth.isOnDamaged &&
                undergroundMovement.isReached)
            {
                StartCoroutine(playerHealth.OnDamaged());
            }
        }
    }
}
