using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    bool isCollisioned;
    Health health;
    UndergroundMovement undergroundMovement;

    private void Start()
    {
        health = GetComponent<Health>();
        undergroundMovement = GetComponent<UndergroundMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") &&
            !health.isOnDamaged &&
            undergroundMovement.isReached)
        {
            StartCoroutine(health.OnDamaged());
        }
    }
}
