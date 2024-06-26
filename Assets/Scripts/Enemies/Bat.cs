using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private AIPath aIPath;
    private AIDestinationSetter aIDestinationSetter;

    private BasicEnemy enemyHealth;

    [SerializeField]
    private Transform shadow;

    private GameObject player;

    void Start()
    {
        aIPath = GetComponent<AIPath>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();  

        enemyHealth = GetComponent<BasicEnemy>();

        enemyHealth.Init();

        player = GameObject.Find("Player");

        aIDestinationSetter.target = player.transform;
    }

    void Update()
    {
        enemyHealth.ManageHealth();

        if (enemyHealth.isStartMove)
        {
            if (!enemyHealth.isDead)
            {
                shadow.localPosition = new Vector3(0, 0, 0);
                aIPath.canMove = true;
                FlipX();
            }
            else
            {
                shadow.localPosition = new Vector3(0, 0.25f, 0);
                aIPath.canMove = false;
            }
        }
    }

    private void FlipX()
    {
        if (player != null)
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

    private void OnDisable()
    {
        if (enemyHealth.isStartMove)
        {
            if (!enemyHealth.isDead)
            {
                shadow.localPosition = new Vector3(0, 0, 0);
                aIPath.canMove = true;
                FlipX();
            }
            else
            {
                shadow.localPosition = new Vector3(0, 0.25f, 0);
                aIPath.canMove = false;
            }
        }
    }
}
