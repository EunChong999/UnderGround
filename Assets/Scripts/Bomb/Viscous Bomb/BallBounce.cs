using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    private Rigidbody2D rb;

    Vector3 lastVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

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
