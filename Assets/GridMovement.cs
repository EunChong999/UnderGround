using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public Transform movePoint;
    public LayerMask whatStopMovement;
    public float x;
    public float y;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(x, 0, 0), .2f, whatStopMovement))
        {
            movePoint.position += new Vector3(x, 0, 0);
        }

        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, y, 0), .2f, whatStopMovement))
        {
            movePoint.position += new Vector3(0, y, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(movePoint.position, 0.2f);
    }
}
