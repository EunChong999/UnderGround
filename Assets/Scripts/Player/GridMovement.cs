using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public bool isMoveType;
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
        MoveGrid();
    }

    void MoveGrid()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (isMoveType)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(x, 0, 0), .25f, whatStopMovement))
            {
                isCollision = false;
                movePoint.position += new Vector3(x, 0, 0);
            }
            else
            {
                isCollision = true;
            }

            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, y, 0), .25f, whatStopMovement))
            {
                isCollision = false;
                movePoint.position += new Vector3(0, y, 0);
            }
            else
            {
                isCollision = true;
            }
        }
        else
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(x * Time.deltaTime * moveSpeed, 0, 0), .25f, whatStopMovement))
            {
                isCollision = false;
                movePoint.position += new Vector3(x * Time.deltaTime * moveSpeed, 0, 0);
            }
            else
            {
                isCollision = true;
            }

            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, y * Time.deltaTime * moveSpeed, 0), .25f, whatStopMovement))
            {
                isCollision = false;
                movePoint.position += new Vector3(0, y * Time.deltaTime * moveSpeed, 0);
            }
            else
            {
                isCollision = true;
            }
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(movePoint.position, 0.25f);
    }
}
