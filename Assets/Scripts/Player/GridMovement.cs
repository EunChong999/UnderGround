using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public bool isMoveType;
    public float moveSpeed = 5;
    public Transform movePoint;
    public LayerMask whatStopMovement;
    public float x;
    public float y;
    [SerializeField] float delayTime;
    WaitForSeconds waitForSeconds;
    UndergroundMovement undergroundMovement;
    bool canMove;

    void Start()
    {
        waitForSeconds = new WaitForSeconds(delayTime);
        canMove = true;
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

        if (isMoveType)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(x, 0, 0), .25f, whatStopMovement) && canMove && x != 0)
            {
                movePoint.position += new Vector3(x, 0, 0);
                StartCoroutine(DelayMove());
                canMove = false;
            }

            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, y, 0), .25f, whatStopMovement) && canMove && y != 0)
            {
                movePoint.position += new Vector3(0, y, 0);
                StartCoroutine(DelayMove());
                canMove = false;
            }
        }
        else
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(x * Time.deltaTime * moveSpeed, 0, 0), .25f, whatStopMovement))
            {
                movePoint.position += new Vector3(x * Time.deltaTime * moveSpeed, 0, 0);
            }

            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, y * Time.deltaTime * moveSpeed, 0), .25f, whatStopMovement))
            {
                movePoint.position += new Vector3(0, y * Time.deltaTime * moveSpeed, 0);
            }
        }
    }

    IEnumerator DelayMove()
    {
        yield return waitForSeconds;
        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(movePoint.position, 0.25f);
    }
}
