using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isChangingGravity = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ChangeGravity();

        if (rb.velocity.x == 0 && rb.velocity.y == 0)
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                Physics2D.gravity = new Vector2(0f, 10f);
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                transform.eulerAngles = new Vector3(0, 0, 180f);
                // RotateCameraMethod
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Physics2D.gravity = new Vector2(-10f, 0f);
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                transform.eulerAngles = new Vector3(0, 0, 270f);
                // RotateCameraMethod
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Physics2D.gravity = new Vector2(0f, -10f);
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                transform.eulerAngles = new Vector3(0, 0, 0f);
                // RotateCameraMethod
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Physics2D.gravity = new Vector2(10f, 0f);
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                transform.eulerAngles = new Vector3(0, 0, 90f);
                // RotateCameraMethod
            }
        }
    }
}

