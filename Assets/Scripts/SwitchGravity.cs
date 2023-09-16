using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGravity : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    public Transform[] spaceCheck;  
    [SerializeField] private LayerMask groundLayer;

    [HideInInspector] public bool isChangingGravity;
    private Rigidbody2D rb;
    private RotateCamera rotationCamera;
    private GridMovement gridMovement;
    private Animator animator;

    void Start()
    {
        isChangingGravity = false;
        rb = GetComponent<Rigidbody2D>();
        rotationCamera = GameObject.Find("Virtual Camera").GetComponent<RotateCamera>();
        gridMovement = GetComponent<GridMovement>();
        animator = transform.GetChild(0).GetComponent<Animator>();
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
        GroundCheck();
        ChangeGravity();
    }

    void GroundCheck()
    {
        if (IsGrounded(groundCheck))  
        {
            isChangingGravity = false;
        }
        else
        {
            isChangingGravity = true;
        }
    }

    void ChangeGravity()
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
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (transform.eulerAngles.z == 90)
                {
                    gridMovement.x = -1;
                    gridMovement.y = 0;
                    transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (transform.eulerAngles.z == 180)
                {
                    gridMovement.x = 0;
                    gridMovement.y = -1;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.eulerAngles.z == 270)
                {
                    gridMovement.x = 1;
                    gridMovement.y = 0;
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }

                rotationCamera.ChangeRotate(transform.eulerAngles.z);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetBool("IsHorizontal", true);

                if (transform.eulerAngles.z == 0)
                {
                    gridMovement.x = -1;
                    gridMovement.y = 0;
                    transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (transform.eulerAngles.z == 90)
                {
                    gridMovement.x = 0;
                    gridMovement.y = -1;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.eulerAngles.z == 180)
                {
                    gridMovement.x = 1;
                    gridMovement.y = 0;
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (transform.eulerAngles.z == 270)
                {
                    gridMovement.x = 0;
                    gridMovement.y = 1;
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }

                rotationCamera.ChangeRotate(transform.eulerAngles.z);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetBool("IsHorizontal", true);

                if (transform.eulerAngles.z == 0)
                {
                    gridMovement.x = 1;
                    gridMovement.y = 0;
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (transform.eulerAngles.z == 90)
                {
                    gridMovement.x = 0;
                    gridMovement.y = 1;
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (transform.eulerAngles.z == 180)
                {
                    gridMovement.x = -1;
                    gridMovement.y = 0;
                    transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (transform.eulerAngles.z == 270)
                {
                    gridMovement.x = 0;
                    gridMovement.y = -1;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }

                rotationCamera.ChangeRotate(transform.eulerAngles.z);
            }
            else
            {
                animator.SetBool("IsHorizontal", false);
                animator.SetBool("IsVertical", false);
            }
        }
    }
}

