using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class UndergroundMovement : MonoBehaviour
{
    public Transform[] spaceCheck;
    [SerializeField] 
    private LayerMask groundLayer;
    public bool[] isLockedKey;

    public bool[] isSpaced;
    private Rigidbody2D rb;
    [HideInInspector]
    public GridMovement gridMovement;
    PlayerHealth playerHealth;
    public Transform direction;
    [HideInInspector]
    public bool isMoveStart;
    public bool isStopped;
    public bool isReached;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gridMovement = GetComponent<GridMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        isMoveStart = false;
    }

    public bool IsGrounded(Transform transform)
    {
        return Physics2D.OverlapCircle(transform.position, 0.25f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(spaceCheck[0].position, 0.25f);
        Gizmos.DrawSphere(spaceCheck[1].position, 0.25f);
        Gizmos.DrawSphere(spaceCheck[2].position, 0.25f);
        Gizmos.DrawSphere(spaceCheck[3].position, 0.25f);
    }

    void Update()
    {
        if (isMoveStart)
        {
            isSpaced[0] = GroundCheck(spaceCheck[0]);
            isSpaced[1] = GroundCheck(spaceCheck[1]);
            isSpaced[2] = GroundCheck(spaceCheck[2]);
            isSpaced[3] = GroundCheck(spaceCheck[3]);

            isReached = (isStopped && transform.position.x % 1 == 0 && transform.position.y % 1 == 0);
        }

        if (Time.timeScale != 0) 
        Move();

        ManageSound();
    }

    bool GroundCheck(Transform direction)
    {
        if (IsGrounded(direction) && transform.position.x % 1 == 0 && transform.position.y % 1 == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void Move()
    {
        if (!playerHealth.isDead)
        {
            if(isReached)
            {
                if (Input.GetKeyDown(KeyCode.W) && (isSpaced[0] || !isMoveStart) && !isLockedKey[0])
                {
                    isMoveStart = true;
                    direction = spaceCheck[0];
                    gridMovement.x = 0;
                    gridMovement.y = 1;
                    isStopped = false;
                }
                else if (Input.GetKeyDown(KeyCode.A) && (isSpaced[1] || !isMoveStart) && !isLockedKey[1])
                {
                    isMoveStart = true;
                    direction = spaceCheck[1];
                    gridMovement.x = -1;
                    gridMovement.y = 0;
                    isStopped = false;
                }
                else if (Input.GetKeyDown(KeyCode.S) && (isSpaced[2] || !isMoveStart) && !isLockedKey[2])
                {
                    isMoveStart = true;
                    direction = spaceCheck[2];
                    gridMovement.x = 0;
                    gridMovement.y = -1;
                    isStopped = false;
                }
                else if (Input.GetKeyDown(KeyCode.D) && (isSpaced[3] || !isMoveStart) && !isLockedKey[3])
                {
                    isMoveStart = true;
                    direction = spaceCheck[3];
                    gridMovement.x = 1;
                    gridMovement.y = 0;
                    isStopped = false;
                }
            }
        }
    }

    void ManageSound()
    {
        if (!isStopped)
        {
            GetComponent<AudioManager>().Play("Dig");
        }
        else
        {
            GetComponent<AudioManager>().Stop("Dig");
        }
    }
}