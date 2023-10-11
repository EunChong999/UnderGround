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
        if (!health.isDead)
        {
            for (int i = 0; i < obstacles.Length; i++)
            {
                if (collision.name.Contains(obstacles[i].name) &&
                    !health.isOnDamaged &&
                    !switchGravity.isMoving)
                {
                    StartCoroutine(health.OnDamaged());
                }
            }
        }
    }
}
