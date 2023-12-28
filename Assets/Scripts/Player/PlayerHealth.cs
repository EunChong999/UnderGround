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

        // 현재 색상 정보 가져오기
        Color spriteColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        // 새로운 알파 값 설정 
        float newAlpha = 0.5f;
        spriteColor.a = newAlpha;

        // 변경된 색상 적용
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = spriteColor;

        animator.SetBool("IsDead", false);

        animator.SetTrigger("Damage");

        yield return new WaitForSeconds(coolTime);

        isOnDamaged = false;

        // 새로운 알파 값 설정 
        newAlpha = 1;
        spriteColor.a = newAlpha;

        // 변경된 색상 적용
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = spriteColor;
    }

    public void Dead()
    {
        if (!isDead) 
        {
            LevelManager.Instance.LoadGamePlayScene();
        }

        isDead = true;

        // 현재 색상 정보 가져오기
        Color spriteColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        // 새로운 알파 값 설정 
        float newAlpha = 1;
        spriteColor.a = newAlpha;

        // 변경된 색상 적용
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = spriteColor;

        animator.SetBool("IsDead", true);
    }
}
