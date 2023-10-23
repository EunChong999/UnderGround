using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public bool isDead;
    public float coolTime;

    private Animator animator;

    [SerializeField]
    private float waitTime;

    private WaitForSeconds waitForSeconds;

    private bool isStartMove;

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("IsDead", false);
        animator.SetTrigger("Appear");
        waitForSeconds = new WaitForSeconds(waitTime);
        isStartMove = false;
        StartCoroutine(StartMove());
    }

    IEnumerator StartMove()
    {
        yield return waitForSeconds;
        isStartMove = true;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        isDead = true;

        animator.SetBool("IsDead", true);
    }
}
