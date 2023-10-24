using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerCheck : MonoBehaviour
{
    [SerializeField]
    private AIPath aIPath;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.name.Contains("Explosion"))
        {
            aIPath.canMove = true;
        }
        else
        {
            aIPath.canMove = false;
        }
    }
}
