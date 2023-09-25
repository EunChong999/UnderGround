using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class SwitchGravity : MonoBehaviour
{
    public Transform[] spaceCheck;
    Transform direction;
    [SerializeField] private LayerMask groundLayer;

    [HideInInspector] public bool isChangingGravity;
    private Rigidbody2D rb;
    private GridMovement gridMovement;
    private Animator animator;
    Health health;

    void Start()
    {
        direction = spaceCheck[2];

        isChangingGravity = false;
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
        GroundCheck(direction);
        ChangeGravity();
    }

    void GroundCheck(Transform transform)
    {
        if (IsGrounded(transform) && transform.position.x % 1 == 0 && transform.position.y % 1 == 0)
        {
            animator.SetBool("isWalking", false);
            isChangingGravity = false;
        }
        else
        {
            animator.SetBool("isWalking", true);
            isChangingGravity = true;
        }
    }

    void ChangeGravity()
    {
        if (!health.isDead)
        {
            if (!isChangingGravity)
            {
                if (Input.GetKeyDown(KeyCode.W)) // Complete
                {
                    direction = spaceCheck[0];

                    animator.SetFloat("horizontal", 0);
                    animator.SetFloat("vertical", 1);

                    gridMovement.x = 0;
                    gridMovement.y = 1;
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    direction = spaceCheck[1];

                    animator.SetFloat("horizontal", -1);
                    animator.SetFloat("vertical", 0);

                    gridMovement.x = -1;
                    gridMovement.y = 0;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    direction = spaceCheck[2];

                    animator.SetFloat("horizontal", 0);
                    animator.SetFloat("vertical", -1);

                    gridMovement.x = 0;
                    gridMovement.y = -1;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    direction = spaceCheck[3];

                    animator.SetFloat("horizontal", 1);
                    animator.SetFloat("vertical", 0);

                    gridMovement.x = 1;
                    gridMovement.y = 0;
                }
            }
        }
        else
        {
            // 플레이어 사망 시 제일 마지막에 저장된 중력의 방향으로 떨어짐
            if (isChangingGravity)
            {
                gridMovement.x = 0;
                gridMovement.y = -1;
            }
        }
    }
}

