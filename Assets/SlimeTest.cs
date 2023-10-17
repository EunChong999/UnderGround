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
    private Transform groundCheckerPos;
    [SerializeField]
    private Transform wallCheckerPos;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private float wallCheckDistance;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
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
        // 슬라임은 총 4방향으로 움직인다.
        // 1.그라운드 체커 다운, 월 체커 라이트 <- 시작할 때 이 경우를 따른다.
        // 2.그라운드 체커 라이트, 월 체커 업
        // 3.그라운드 체커 업, 월 체커 레프트
        // 4.그라운드 체커 레프트, 월 체커 다운

        // 예외로, 그라운드 체커와 월 체커 모두 false면,
        // 그에 따른 조치를 취하여, 무작정 오른쪽으로만 이동한다.
    }

    void Movement()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheckerPos.position, new Vector2(groundCheckerPos.position.x, groundCheckerPos.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheckerPos.position, new Vector2(wallCheckerPos.position.x + wallCheckDistance, wallCheckerPos.position.y));
    }
}
