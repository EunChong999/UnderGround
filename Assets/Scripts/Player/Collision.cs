using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    bool isCollisioned;
    Health health;
    SwitchGravity switchGravity;

    private void Start()
    {
        health = transform.parent.GetComponent<Health>();
        switchGravity = transform.parent.GetComponent<SwitchGravity>();
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
