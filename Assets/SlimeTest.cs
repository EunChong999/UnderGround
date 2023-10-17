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
        // �������� �� 4�������� �����δ�.
        // 1.�׶��� üĿ �ٿ�, �� üĿ ����Ʈ <- ������ �� �� ��츦 ������.
        // 2.�׶��� üĿ ����Ʈ, �� üĿ ��
        // 3.�׶��� üĿ ��, �� üĿ ����Ʈ
        // 4.�׶��� üĿ ����Ʈ, �� üĿ �ٿ�

        // ���ܷ�, �׶��� üĿ�� �� üĿ ��� false��,
        // �׿� ���� ��ġ�� ���Ͽ�, ������ ���������θ� �̵��Ѵ�.
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
