using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool groundDetected;
    [SerializeField]
    private bool wallDetected;
    [SerializeField]
    private Transform groundPositionChecker;
    [SerializeField]
    private Transform wallPositionChecker;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private float wallCheckDistance;
    [SerializeField]
    private LayerMask whatIsGround;
    private bool hasTurn;
    private float zAxisAdd;
    [SerializeField]
    private int direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hasTurn = false;
    }

    private void Update()
    {
        CheckGroundOrWall();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void CheckGroundOrWall()
    {
        groundDetected = Physics2D.Raycast(groundPositionChecker.position, -transform.up, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallPositionChecker.position, transform.right, wallCheckDistance, whatIsGround);

        if (!groundDetected)
        {
            if (!hasTurn)
            {
                if (wallDetected)
                {
                    zAxisAdd += 90;
                }
                else
                {
                    zAxisAdd -= 90;
                }

                transform.eulerAngles = new Vector3(0, 0, zAxisAdd);

                hasTurn = true;
            }
        }

        if (groundDetected)
        {
            hasTurn = false;
        }

        if (wallDetected)
        {
            if (!hasTurn)
            {
                if (!groundDetected)
                {
                    Debug.Log("Here");
                    zAxisAdd -= 90;
                }
                else
                {
                    zAxisAdd += 90;
                }

                transform.eulerAngles = new Vector3(0, 0, zAxisAdd);

                hasTurn = true;
            }
        }

        if (zAxisAdd <= -360)
        {
            zAxisAdd = 0;
        }

        if (zAxisAdd >= 360)
        {
            zAxisAdd = 0;
        }
    }

    void Movement()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundPositionChecker.position, new Vector2(groundPositionChecker.position.x, groundPositionChecker.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallPositionChecker.position, new Vector2(wallPositionChecker.position.x + wallCheckDistance, wallPositionChecker.position.y));
    }
}

