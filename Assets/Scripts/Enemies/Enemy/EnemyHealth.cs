using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public bool isDead;

    private Animator animator;

    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float appearRange;

    private WaitForSeconds waitForSeconds;
    private float distance;
    private GameObject player;
    private GameObject body;
    private new Collider2D collider2D;

    [HideInInspector]
    public bool isAppeared;

    public void Init()
    {
        isAppeared = false;
        isDead = false;
        animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("IsDead", false);
        player = GameObject.Find("Player");
        body = transform.GetChild(0).gameObject;
        body.SetActive(false);
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        waitForSeconds = new WaitForSeconds(waitTime);
    }

    public void Appear()
    {
        body.SetActive(true);
        animator.SetTrigger("Appear");
        StartCoroutine(StartMove());
        isAppeared = true;
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
        collider2D.enabled = true;
    }

    public void Dead()
    {
        isDead = true;
        collider2D.enabled = false;
        animator.SetBool("IsDead", true);

        if (transform.childCount > 0) 
        {
            transform.GetChild(0).parent = null;
        }

        gameObject.SetActive(false);
    }
}
