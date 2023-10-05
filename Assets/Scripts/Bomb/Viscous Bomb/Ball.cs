using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    public bool isFixedX;
    public bool isFixedY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (isFixedX)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        if (isFixedY)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        rb.AddForce(new Vector2(speed, speed) * Time.deltaTime * 10000);
    }
}
