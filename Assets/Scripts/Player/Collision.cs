using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    bool isCollisioned;
    Health health;
    SwitchGravity switchGravity;

    private void Start()
    {
        health = GetComponent<Health>();
        switchGravity = GetComponent<SwitchGravity>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") &&
            !health.isOnDamaged &&
            !switchGravity.isMoving)
        {
            StartCoroutine(health.OnDamaged());
        }
    }
}
