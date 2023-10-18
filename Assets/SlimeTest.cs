using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTest : MonoBehaviour
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
    private Vector3[] groundPositionCheckerPos;
    [SerializeField]
    private Transform wallPositionChecker;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private float wallCheckDistance;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private bool hasTurn;
    [SerializeField]
    private float angle;
    [SerializeField]
    private Transform checkers;
    [SerializeField]
    private Vector2 groundCheckDir;
    [SerializeField]
    private Vector2 wallCheckDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hasTurn = false;
        checkers.parent = null;
    }
    private void Update()
    {
        checkers.position = transform.position;
        CheckGroundOrWall();
        ChangeAngle();
    }
    private void FixedUpdate()
    {
        Movement();
    }

    void PosRound()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float x = Mathf.Round(position.x);
        float y = Mathf.Round(position.y);
        float z = Mathf.Round(position.z);
        transform.position = new Vector3(x, y, z);
    }

    void CheckGroundOrWall()
    {
        if (angle == 0 || angle == -360)
        {
            groundCheckDir = -transform.up;
            wallCheckDir = transform.right;
            groundPositionChecker.localPosition = groundPositionCheckerPos[0];
        }
        else if (angle == 270 || angle == -90)
        {
            groundCheckDir = -transform.up;
            wallCheckDir = transform.right;
            groundPositionChecker.localPosition = groundPositionCheckerPos[1];
        }
        else if (angle == 180 || angle == -180)
        {
            groundCheckDir = -transform.up;
            wallCheckDir = transform.right;
            groundPositionChecker.localPosition = groundPositionCheckerPos[2];
        }
        else if (angle == 90 || angle == -270)
        {
            groundCheckDir = -transform.up;
            wallCheckDir = transform.right;
            groundPositionChecker.localPosition = groundPositionCheckerPos[3];
        }

        groundDetected = Physics2D.Raycast(groundPositionChecker.position, groundCheckDir, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallPositionChecker.position, wallCheckDir, wallCheckDistance, whatIsGround);
    }

    void ChangeAngle()
    {
        if (groundDetected)
        {
            hasTurn = false;
        }

        if (angle <= -360)
        {
            angle = 0;
        }

        if (angle >= 360)
        {
            angle = 0;
        }

        if (!groundDetected)
        {
            if (!hasTurn)
            {
                PosRound();
                angle -= 90;
                transform.eulerAngles = new Vector3(0, 0, angle);
                hasTurn = true;
            }
            else
            {
                if (!hasTurn)
                {
                    PosRound();
                    angle += 90;
                    transform.eulerAngles = new Vector3(0, 0, angle);
                    hasTurn = true;
                }
            }
        }
    }

    void Movement()
    {
        rb.velocity = transform.right * speed * Time.fixedDeltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundPositionChecker.position, new Vector2(groundPositionChecker.position.x, groundPositionChecker.position.y) + groundCheckDir * groundCheckDistance);
        Gizmos.DrawLine(wallPositionChecker.position, new Vector2(wallPositionChecker.position.x, wallPositionChecker.position.y) + wallCheckDir * wallCheckDistance);
    }
}
