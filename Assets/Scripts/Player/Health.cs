using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public bool isOnDamaged;
    public bool isDead;
    public float coolTime;

    Animator animator;
    MonoBehaviour[] monoBehaviours;

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();

        monoBehaviours = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour != null && !monoBehaviour.GetType().Name.Contains("Health"))
            {
                Debug.Log("스크립트 이름 : " + monoBehaviour.GetType().Name);
            }
        }
    }

    void Update()
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

    public IEnumerator OnDamaged()
    {
        health--;

        isOnDamaged = true;

        // 현재 색상 정보 가져오기
        Color spriteColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        // 새로운 알파 값 설정 
        float newAlpha = 0.5f;
        spriteColor.a = newAlpha;

        // 변경된 색상 적용
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = spriteColor;

        Debug.Log("스프라이트의 새로운 알파 값: " + transform.GetChild(0).GetComponent<SpriteRenderer>().color.a);

        animator.SetTrigger("IsDamage");

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
        isDead = true;

        // 현재 색상 정보 가져오기
        Color spriteColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        // 새로운 알파 값 설정 
        float newAlpha = 1;
        spriteColor.a = newAlpha;

        // 변경된 색상 적용
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = spriteColor;

        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour != null && !monoBehaviour.GetType().Name.Contains("Health")) 
            {
                monoBehaviour.enabled = false;
            }
        }

        animator.SetTrigger("IsDead");
    }
}
