using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class SwitchGravity : MonoBehaviour
{
    Vector3 currentAngle;
    [SerializeField] private float lerpTime;
    [SerializeField] private float currentTime;

    [SerializeField] private Transform groundCheck;
    public Transform[] spaceCheck;
    [SerializeField] private LayerMask groundLayer;

    [HideInInspector] public bool isChangingGravity;
    private Rigidbody2D rb;
    private RotateCamera rotationCamera;
    private GridMovement gridMovement;
    private Animator animator;
    Health health;

    void Start()
    {
        isChangingGravity = false;
        rb = GetComponent<Rigidbody2D>();
        rotationCamera = GameObject.Find("Virtual Camera").GetComponent<RotateCamera>();
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
        Gizmos.DrawSphere(groundCheck.position, 0.25f);
        Gizmos.DrawSphere(spaceCheck[0].position, 0.25f);
        Gizmos.DrawSphere(spaceCheck[1].position, 0.25f);
        Gizmos.DrawSphere(spaceCheck[2].position, 0.25f);
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= lerpTime)
        {
            currentTime = lerpTime;
        }

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, currentAngle, currentTime / lerpTime);

        GroundCheck();
        ChangeGravity();
    }

    void GroundCheck()
    {
        if (IsGrounded(groundCheck) && transform.position.x % 1 == 0 && transform.position.y % 1 == 0)
        {
            isChangingGravity = false;
        }
        else
        {
            isChangingGravity = true;
        }
    }

    public void ChangeRotate(float rot)
    {
        currentTime = 0;
        currentAngle = new Vector3(0, 0, rot);
    }

    void ChangeGravity()
    {
        if (!health.isDead)
        {
            if (!isChangingGravity)
            {
                if (Input.GetKeyDown(KeyCode.W)) // Complete
                {
                    animator.SetBool("IsVertical", true);

                    if (transform.eulerAngles.z == 0)
                    {
                        gridMovement.x = 0;
                        gridMovement.y = 1;
                        //transform.eulerAngles = new Vector3(0, 0, 180);
                        ChangeRotate(180);
                        rotationCamera.ChangeRotate(180);
                    }
                    else if (transform.eulerAngles.z == 90)
                    {
                        gridMovement.x = -1;
                        gridMovement.y = 0;
                        //transform.eulerAngles = new Vector3(0, 0, 270);
                        ChangeRotate(270);
                        rotationCamera.ChangeRotate(270);
                    }
                    else if (transform.eulerAngles.z == 180)
                    {
                        gridMovement.x = 0;
                        gridMovement.y = -1;
                        //transform.eulerAngles = new Vector3(0, 0, 0);
                        ChangeRotate(0);
                        rotationCamera.ChangeRotate(0);
                    }
                    else if (transform.eulerAngles.z == 270)
                    {
                        gridMovement.x = 1;
                        gridMovement.y = 0;
                        //transform.eulerAngles = new Vector3(0, 0, 90);
                        ChangeRotate(90);
                        rotationCamera.ChangeRotate(90);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    animator.SetBool("IsHorizontal", true);

                    if (transform.eulerAngles.z == 0)
                    {
                        gridMovement.x = -1;
                        gridMovement.y = 0;
                        //transform.eulerAngles = new Vector3(0, 0, 270);
                        ChangeRotate(270);
                        rotationCamera.ChangeRotate(270);
                    }
                    else if (transform.eulerAngles.z == 90)
                    {
                        gridMovement.x = 0;
                        gridMovement.y = -1;
                        //transform.eulerAngles = new Vector3(0, 0, 0);
                        ChangeRotate(0);
                        rotationCamera.ChangeRotate(0);
                    }
                    else if (transform.eulerAngles.z == 180)
                    {
                        gridMovement.x = 1;
                        gridMovement.y = 0;
                        //transform.eulerAngles = new Vector3(0, 0, 90);
                        ChangeRotate(90);
                        rotationCamera.ChangeRotate(90);
                    }
                    else if (transform.eulerAngles.z == 270)
                    {
                        gridMovement.x = 0;
                        gridMovement.y = 1;
                        //transform.eulerAngles = new Vector3(0, 0, 180);
                        ChangeRotate(180);
                        rotationCamera.ChangeRotate(180);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    animator.SetBool("IsHorizontal", true);

                    if (transform.eulerAngles.z == 0)
                    {
                        gridMovement.x = 1;
                        gridMovement.y = 0;
                        //transform.eulerAngles = new Vector3(0, 0, 90);
                        ChangeRotate(90);
                        rotationCamera.ChangeRotate(90);
                    }
                    else if (transform.eulerAngles.z == 90)
                    {
                        gridMovement.x = 0;
                        gridMovement.y = 1;
                        //transform.eulerAngles = new Vector3(0, 0, 180);
                        ChangeRotate(180);
                        rotationCamera.ChangeRotate(180);
                    }
                    else if (transform.eulerAngles.z == 180)
                    {
                        gridMovement.x = -1;
                        gridMovement.y = 0;
                        //transform.eulerAngles = new Vector3(0, 0, 270);
                        ChangeRotate(270);
                        rotationCamera.ChangeRotate(270);
                    }
                    else if (transform.eulerAngles.z == 270)
                    {
                        gridMovement.x = 0;
                        gridMovement.y = -1;
                        //transform.eulerAngles = new Vector3(0, 0, 0);
                        ChangeRotate(0);
                        rotationCamera.ChangeRotate(0);
                    }
                }
                else
                {
                    animator.SetBool("IsHorizontal", false);
                    animator.SetBool("IsVertical", false);
                }
            }
        }
        else
        {
            // 플레이어 사망 시 제일 마지막에 저장된 중력의 방향으로 떨어짐
            if (isChangingGravity)
            {
                if (transform.eulerAngles.z == 0)
                {
                    gridMovement.x = 0;
                    gridMovement.y = -1;
                }
                else if (transform.eulerAngles.z == 90)
                {
                    gridMovement.x = 1;
                    gridMovement.y = 0;
                }
                else if (transform.eulerAngles.z == 180)
                {
                    gridMovement.x = 0;
                    gridMovement.y = 1;
                }
                else if (transform.eulerAngles.z == 270)
                {
                    gridMovement.x = -1;
                    gridMovement.y = 0;
                }
            }
        }
    }
}

