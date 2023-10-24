using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosionCheck : MonoBehaviour
{
    [SerializeField]
    private AIPath aIPath;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Contains("Explosion"))
        {
            aIPath.canMove = false;
        }
        else
        {
            aIPath.canMove = true;
        }
    }
}
