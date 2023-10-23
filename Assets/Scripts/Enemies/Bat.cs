using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private AIPath aIPath;

    private EnemyHealth enemyHealth;

    [SerializeField]
    private Transform shadow;

    private GameObject player;

    private CircleCollider2D circleCollider;

    void Start()
    {
        aIPath = GetComponent<AIPath>();

        enemyHealth = GetComponent<EnemyHealth>();

        player = GameObject.Find("Player");

        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (!enemyHealth.isDead)
        {
            shadow.localPosition = new Vector3(0, 0, 0);
            aIPath.canMove = true;
            circleCollider.enabled = true;
            FlipX();
        }
        else
        {
            shadow.localPosition = new Vector3(0, 0.25f, 0);
            circleCollider.enabled = false;
            aIPath.canMove = false;
        }
    }

    private void FlipX()
    {
        transform.rotation = player.transform.rotation;

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
