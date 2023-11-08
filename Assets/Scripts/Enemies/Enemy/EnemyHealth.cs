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

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("IsDead", false);
        player = GameObject.Find("Player");
        body = transform.GetChild(0).gameObject;
        body.SetActive(false);
        collider2D = GetComponent<Collider2D>();    
        collider2D.enabled = false;
        waitForSeconds = new WaitForSeconds(waitTime);
        isAppeared = false;
    }

    private void Appear()
    {
        body.SetActive(true);
        animator.SetTrigger("Appear");
        StartCoroutine(StartMove());
        isAppeared = true;
    }

    IEnumerator StartMove()
    {
        yield return waitForSeconds;
        collider2D.enabled = true;
    }

    private void Update()
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

    public void Dead()
    {
        isDead = true;
        collider2D.enabled = false;
        animator.SetBool("IsDead", true);
    }
}
