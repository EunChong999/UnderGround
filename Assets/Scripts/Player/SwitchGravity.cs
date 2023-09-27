using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class SwitchGravity : MonoBehaviour
{
    public Transform[] spaceCheck;
    [SerializeField] private LayerMask groundLayer;

    public bool[] isSpaced;
    private Rigidbody2D rb;
    private GridMovement gridMovement;
    private Animator animator;
    Health health;
    public Transform direction;
    [HideInInspector] public bool isMoving;

    void Start()
    {
        direction = spaceCheck[2];
        rb = GetComponent<Rigidbody2D>();
        gridMovement = GetComponent<GridMovement>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        health = GetComponent<Health>();
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
        isSpaced[0] = GroundCheck(spaceCheck[0]);
        isSpaced[1] = GroundCheck(spaceCheck[1]);
        isSpaced[2] = GroundCheck(spaceCheck[2]);
        isSpaced[3] = GroundCheck(spaceCheck[3]);

        isMoving = GroundCheck(direction);

        ChangeGravity();
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

    void ChangeGravity()
    {
        if (!health.isDead)
        {
            if(!isMoving)
            {
                if (Input.GetKeyDown(KeyCode.W) && isSpaced[0])
                {
                    direction = spaceCheck[0];
                    gridMovement.x = 0;
                    gridMovement.y = 1;
                }
                else if (Input.GetKeyDown(KeyCode.A) && isSpaced[1])
                {
                    direction = spaceCheck[1];
                    gridMovement.x = -1;
                    gridMovement.y = 0;
                }
                else if (Input.GetKeyDown(KeyCode.S) && isSpaced[2])
                {
                    direction = spaceCheck[2];
                    gridMovement.x = 0;
                    gridMovement.y = -1;
                }
                else if (Input.GetKeyDown(KeyCode.D) && isSpaced[3])
                {
                    direction = spaceCheck[3];
                    gridMovement.x = 1;
                    gridMovement.y = 0;
                }
            }
        }
    }
}