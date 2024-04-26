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
    UndergroundMovement undergroundMovement;
    float curXPos;
    float curYPos;

    void Start()
    {
        movePoint.parent = null;
        undergroundMovement = GetComponent<UndergroundMovement>();
    }

    void Update()
    {
        if (isMoveType)
        {
            if (undergroundMovement.isMoveStart)
            {
                MoveGrid();
            }
        }
        else
        {
            MoveGrid();
        }
    }

    void MoveGrid()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        curXPos = x * Time.deltaTime * moveSpeed;
        curYPos = y * Time.deltaTime * moveSpeed;

        if (isMoveType)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(x, 0, 0), .25f, whatStopMovement) && x != 0)
            {
                movePoint.position += new Vector3(x, 0, 0);
            }

            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, y, 0), .25f, whatStopMovement) && y != 0)
            {
                movePoint.position += new Vector3(0, y, 0);
            }
        }
        else
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(curXPos, 0, 0), .25f, whatStopMovement))
            {
                movePoint.position += new Vector3(curXPos, 0, 0);
            }

            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, curYPos, 0), .25f, whatStopMovement))
            {
                movePoint.position += new Vector3(0, curYPos, 0);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(movePoint.position, 0.25f);
    }
}
