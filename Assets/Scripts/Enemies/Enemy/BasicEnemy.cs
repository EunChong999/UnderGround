using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int health;
    public bool isDead;

    private Animator animator;

    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float appearRange;
    [SerializeField]
    private bool isLeaveDeadBody;

    private WaitForSeconds waitForSeconds;
    private float distance;
    private GameObject player;
    private GameObject body;
    private GameObject sign;
    private new Collider2D collider2D;

    [HideInInspector]
    public bool isAppeared;

    [HideInInspector]
    public bool isStartMove;

    public void Init()
    {
        isAppeared = false;
        isStartMove = false;
        isDead = false;
        animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("IsDead", false);
        player = GameObject.Find("Player");
        sign = transform.Find("Sign").gameObject;
        sign.transform.parent = null;
        body = transform.GetChild(0).gameObject;
        body.SetActive(false);
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        waitForSeconds = new WaitForSeconds(waitTime);
    }

    public void Appear()
    {
        isAppeared = true;
        sign.SetActive(false);
        body.SetActive(true);
        animator.SetTrigger("Appear");
        StartCoroutine(StartMove());
    }

    public void ManageHealth()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < appearRange && !isAppeared)
        {
            Appear();
        }

        if (health <= 0)
        {
            Dead();
        }
    }

    IEnumerator StartMove()
    {
        yield return waitForSeconds;
        isStartMove = true;
        collider2D.enabled = true;
    }

    public void Dead()
    {
        isDead = true;
        collider2D.enabled = false;
        animator.SetBool("IsDead", true);

        if (transform.childCount > 0) 
        {
            if (isLeaveDeadBody)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 10;
            }

            transform.GetChild(0).parent = null;
        }

        gameObject.SetActive(false);
    }
}
