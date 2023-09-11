using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGravity : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool isChangingGravity = false;
    private Rigidbody2D rb;
    private RotateCamera rotationCamera;
    private GridMovement gridMovement;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotationCamera = GameObject.Find("Virtual Camera").GetComponent<RotateCamera>();
        gridMovement = GetComponent<GridMovement>();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.25f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, 0.25f);
    }

    void Update()
    {
        GroundCheck();
        ChangeGravity();
    }

    void GroundCheck()
    {
        if (IsGrounded())  
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
        }
    }
}

