using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMover : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool[] detected;
    [SerializeField]
    private Transform[] diagonalChecker;
    [SerializeField]
    private float checkRaius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private bool isRight;

    enum direction { up, left, down, right, none };
    [SerializeField] 
    direction dir;

    [SerializeField] private Transform body;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGroundOrWall();
        Movement();
    }

    void PosRound(float xPos, float yPos)
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float x = Mathf.Round(position.x);
        float y = Mathf.Round(position.y);
        float z = Mathf.Round(position.z);
        transform.position = new Vector3(x + xPos, y + yPos, z);
    }

    void CheckGroundOrWall()
    {
        detected[0] = Physics2D.OverlapBox(diagonalChecker[0].position, new Vector2(checkRaius, checkRaius), 0, whatIsGround);
        detected[1] = Physics2D.OverlapBox(diagonalChecker[1].position, new Vector2(checkRaius, checkRaius), 0, whatIsGround);
        detected[2] = Physics2D.OverlapBox(diagonalChecker[2].position, new Vector2(checkRaius, checkRaius), 0, whatIsGround);
        detected[3] = Physics2D.OverlapBox(diagonalChecker[3].position, new Vector2(checkRaius, checkRaius), 0, whatIsGround);
    }

    void Movement()
    {
        if (isRight)
        {
            if ((detected[0] && detected[3] && detected[2]))
            {
                // Up 이동
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.up * speed * Time.fixedDeltaTime;
                dir = direction.up;
            }
            else
            {
                if ((detected[2] && detected[3]))
                {
                    // Right 이동
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                    rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
                    dir = direction.right;
                }
            }

            if ((detected[0] && detected[1] && detected[3]))
            {
                // Left 이동
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
                dir = direction.left;
            }
            else
            {
                if ((detected[0] && detected[3]))
                {
                    // Up 이동
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                    rb.velocity = Vector2.up * speed * Time.fixedDeltaTime;
                    dir = direction.up;
                }
            }

            if ((detected[1] && detected[2] && detected[0]))
            {
                // Down 이동
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.down * speed * Time.fixedDeltaTime;
                dir = direction.down;
            }
            else
            {
                if ((detected[0] && detected[1]))
                {
                    // Left 이동
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                    rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
                    dir = direction.left;
                }
            }

            if ((detected[2] && detected[3] && detected[1]))
            {
                // Right 이동
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
                dir = direction.right;
            }
            else
            {
                if ((detected[1] && detected[2]))
                {
                    // Down 이동
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                    rb.velocity = Vector2.down * speed * Time.fixedDeltaTime;
                    dir = direction.down;
                }
            }

            if (!detected[0] && !detected[1] && dir == direction.left)
            {
                // Up 이동
                PosRound(0.25f, 0.25f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.up * speed * Time.fixedDeltaTime;
                dir = direction.none;
            }

            if (!detected[1] && !detected[2] && dir == direction.down)
            {
                // Left 이동
                PosRound(-0.25f, 0.25f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
                dir = direction.none;
            }

            if (!detected[2] && !detected[3] && dir == direction.right)
            {
                // Down 이동
                PosRound(-0.25f, -0.25f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.down * speed * Time.fixedDeltaTime;
                dir = direction.none;
            }

            if (!detected[0] && !detected[3] && dir == direction.up)
            {
                // Right 이동
                PosRound(0.25f, -0.25f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
                dir = direction.none;
            }
        }
        else
        {
            if ((detected[1] && detected[2] && detected[3])) 
            {
                // Up 이동
                body.localPosition = new Vector2(0.5f, 0.5f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.up * speed * Time.fixedDeltaTime;
                dir = direction.up;
            }
            else
            {
                if ((detected[2] && detected[3]))
                {
                    // Left 이동
                    body.localPosition = new Vector2(-0.5f, 0.5f);
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                    rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
                    dir = direction.left;
                }
            }

            if ((detected[0] && detected[2] && detected[3])) 
            {
                // Left 이동
                body.localPosition = new Vector2(-0.5f, 0.5f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
                dir = direction.left;
            }
            else
            {
                if ((detected[0] && detected[3]))
                {
                    // Down 이동
                    body.localPosition = new Vector2(-0.5f, -0.5f);
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                    rb.velocity = Vector2.down * speed * Time.fixedDeltaTime;
                    dir = direction.down;
                }
            }

            if ((detected[0] && detected[1] && detected[3])) 
            {
                // Down 이동
                body.localPosition = new Vector2(-0.5f, -0.5f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.down * speed * Time.fixedDeltaTime;
                dir = direction.down;
            }
            else
            {
                if ((detected[0] && detected[1]))
                {
                    // Right 이동
                    body.localPosition = new Vector2(0.5f, -0.5f);
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                    rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
                    dir = direction.right;
                }
            }

            if ((detected[0] && detected[1] && detected[2]))
            {
                // Right 이동
                body.localPosition = new Vector2(0.5f, -0.5f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
                dir = direction.right;
            }
            else
            {
                if ((detected[1] && detected[2]))
                {
                    // Up 이동
                    body.localPosition = new Vector2(0.5f, 0.5f);
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                    rb.velocity = Vector2.up * speed * Time.fixedDeltaTime;
                    dir = direction.up;
                }
            }

            if (!detected[0] && !detected[1] && dir == direction.right)
            {
                // Up 이동
                body.localPosition = new Vector2(0.5f, 0.5f);
                PosRound(-0.25f, 0.25f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.up * speed * Time.fixedDeltaTime;
                dir = direction.none;
            }

            if (!detected[1] && !detected[2] && dir == direction.up)
            {
                // Left 이동
                body.localPosition = new Vector2(-0.5f, 0.5f);
                PosRound(-0.25f, -0.25f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
                dir = direction.none;
            }

            if (!detected[2] && !detected[3] && dir == direction.left)
            {
                // Down 이동
                body.localPosition = new Vector2(-0.5f, -0.5f);
                PosRound(0.25f, -0.25f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.down * speed * Time.fixedDeltaTime;
                dir = direction.none;
            }

            if (!detected[0] && !detected[3] && dir == direction.down)
            {
                // Right 이동
                body.localPosition = new Vector2(0.5f, -0.5f);
                PosRound(0.25f, 0.25f);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
                dir = direction.none;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(diagonalChecker[0].position, new Vector3(checkRaius, checkRaius, 0));
        Gizmos.DrawCube(diagonalChecker[1].position, new Vector3(checkRaius, checkRaius, 0));
        Gizmos.DrawCube(diagonalChecker[2].position, new Vector3(checkRaius, checkRaius, 0));
        Gizmos.DrawCube(diagonalChecker[3].position, new Vector3(checkRaius, checkRaius, 0));
    }
}
