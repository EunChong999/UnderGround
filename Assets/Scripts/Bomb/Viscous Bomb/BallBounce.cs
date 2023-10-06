using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    private Rigidbody2D rb;

    Vector3 lastVelocity;

    private Ball ball;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = GetComponent<Ball>();
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Ball") && collision.CompareTag("Ground"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.zero;

            if (ball.isFixedX)
            {
                direction = Vector3.Reflect(lastVelocity.normalized, collision.transform.up);
            }
            else
            {
                direction = Vector3.Reflect(lastVelocity.normalized, collision.transform.right);
            }

            rb.velocity = direction * Mathf.Max(speed, 0f);

            Vector3 scale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

            scale.x -= 0.025f;
            scale.y -= 0.025f;

            if (scale.x < 0 && scale.y < 0)
            {
                scale.x = 0;
                scale.y = 0;
                gameObject.SetActive(false);
            }

            transform.localScale = scale;
        }
    }
}
