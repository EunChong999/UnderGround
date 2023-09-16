using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public Transform movePoint;
    public LayerMask whatStopMovement;
    [HideInInspector] public float x;
    [HideInInspector] public float y;
    [HideInInspector] public bool isCollision;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(!Physics2D.OverlapCircle(movePoint.position, .2f, whatStopMovement))
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(x, 0, 0), .2f, whatStopMovement))
            {
                isCollision = false;
                movePoint.position += new Vector3(x, 0, 0);
            }
            else
            {
                isCollision = true;
            }

            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, y, 0), .2f, whatStopMovement))
            {
                isCollision = false;
                movePoint.position += new Vector3(0, y, 0);
            }
            else
            {
                isCollision = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(movePoint.position, 0.2f);
    }
}
