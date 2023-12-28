using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public bool isOnDamaged;
    public bool isDead;
    [SerializeField]
    private bool canDamage;
    public float coolTime;

    Animator animator;

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (canDamage)
        {
            if (health > numOfHearts)
            {
                health = numOfHearts;
            }

            if (health <= 0)
            {
                Dead();
            }

            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < health)
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }

                if (i < numOfHearts)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }
    }

    void ManageSound()
    {
        GetComponent<AudioManager>().Play("Damage");
    }

    public IEnumerator OnDamaged()
    {
        ManageSound();

        health--;

        isOnDamaged = true;

        // ���� ���� ���� ��������
        Color spriteColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        // ���ο� ���� �� ���� 
        float newAlpha = 0.5f;
        spriteColor.a = newAlpha;

        // ����� ���� ����
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = spriteColor;

        animator.SetBool("IsDead", false);

        animator.SetTrigger("Damage");

        yield return new WaitForSeconds(coolTime);

        isOnDamaged = false;

        // ���ο� ���� �� ���� 
        newAlpha = 1;
        spriteColor.a = newAlpha;

        // ����� ���� ����
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = spriteColor;
    }

    public void Dead()
    {
        if (!isDead) 
        {
            LevelManager.Instance.LoadGamePlayScene();
        }

        isDead = true;

        // ���� ���� ���� ��������
        Color spriteColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        // ���ο� ���� �� ���� 
        float newAlpha = 1;
        spriteColor.a = newAlpha;

        // ����� ���� ����
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = spriteColor;

        animator.SetBool("IsDead", true);
    }
}
