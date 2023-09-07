using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isChangingGravity = false;
    private RotateCamera rotationCamera;
    [SerializeField] private float speed;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotationCamera = GameObject.Find("Virtual Camera").GetComponent<RotateCamera>();
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
                    Physics2D.gravity = new Vector2(0, speed);
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (transform.eulerAngles.z == 90)
                {
                    Physics2D.gravity = new Vector2(-speed, 0);
                    transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (transform.eulerAngles.z == 180)
                {
                    Physics2D.gravity = new Vector2(0, -speed);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.eulerAngles.z == 270)
                {
                    Physics2D.gravity = new Vector2(speed, 0);
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }

                rotationCamera.ChangeRotate(transform.eulerAngles.z);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (transform.eulerAngles.z == 0)
                {
                    Physics2D.gravity = new Vector2(-speed, 0);
                    transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (transform.eulerAngles.z == 90)
                {
                    Physics2D.gravity = new Vector2(0, -speed);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.eulerAngles.z == 180)
                {
                    Physics2D.gravity = new Vector2(speed, 0);
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (transform.eulerAngles.z == 270)
                {
                    Physics2D.gravity = new Vector2(0, speed);
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }

                rotationCamera.ChangeRotate(transform.eulerAngles.z);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (transform.eulerAngles.z == 0)
                {
                    Physics2D.gravity = new Vector2(speed, 0);
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (transform.eulerAngles.z == 90)
                {
                    Physics2D.gravity = new Vector2(0, speed);
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (transform.eulerAngles.z == 180)
                {
                    Physics2D.gravity = new Vector2(-speed, 0);
                    transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (transform.eulerAngles.z == 270)
                {
                    Physics2D.gravity = new Vector2(0, -speed);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }

                rotationCamera.ChangeRotate(transform.eulerAngles.z);
            }
        }
    }
}

