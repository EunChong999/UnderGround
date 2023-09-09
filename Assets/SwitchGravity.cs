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

    [SerializeField] private List <GameObject> rayObj;

    [SerializeField] float distance;
    RaycastHit2D raycastHit1;
    RaycastHit2D raycastHit2;
    RaycastHit2D raycastHit3;
    RaycastHit2D raycastHit4;

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
        raycastHit1 = Physics2D.Raycast(rayObj[0].transform.position, Vector2.left, distance);
        raycastHit2 = Physics2D.Raycast(rayObj[1].transform.position, Vector2.right, distance);
        raycastHit3 = Physics2D.Raycast(rayObj[2].transform.position, Vector2.up, distance);
        raycastHit4 = Physics2D.Raycast(rayObj[3].transform.position, Vector2.down, distance);

        if (raycastHit1.collider != null) 
        {
            Debug.DrawRay(rayObj[0].transform.position, Vector2.left * raycastHit1.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(rayObj[0].transform.position, Vector2.left * distance, Color.green);
        }

        if (raycastHit2.collider != null)
        {
            Debug.DrawRay(rayObj[1].transform.position, Vector2.right * raycastHit2.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(rayObj[1].transform.position, Vector2.right * distance, Color.green);
        }

        if (raycastHit3.collider != null)
        {
            Debug.DrawRay(rayObj[2].transform.position, Vector2.up * raycastHit3.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(rayObj[2].transform.position, Vector2.up * distance, Color.green);
        }

        if (raycastHit4.collider != null)
        {
            Debug.DrawRay(rayObj[3].transform.position, Vector2.down * raycastHit4.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(rayObj[3].transform.position, Vector2.down * distance, Color.green);
        }

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

