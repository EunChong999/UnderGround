using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    bool isCollisioned;

    private void OnTriggerStay2D(Collider2D collision)
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (collision.name.Contains(obstacles[i].name) && 
                !transform.parent.GetComponent<Health>().isOnDamaged)
            {
                StartCoroutine(transform.parent.GetComponent<Health>().OnDamaged());
            }
        }
    }
}
