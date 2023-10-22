using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] private UndergroundMovement undergroundMovement;

    private void Start()
    {
        undergroundMovement = GameObject.Find("Player").GetComponent<UndergroundMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") && (undergroundMovement.direction == transform))
        {
            undergroundMovement.isStopped = true;
            undergroundMovement.gridMovement.x = 0;
            undergroundMovement.gridMovement.y = 0;
        }
    }
}
