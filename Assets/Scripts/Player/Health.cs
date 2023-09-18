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
                Debug.Log("��ũ��Ʈ �̸� : " + monoBehaviour.GetType().Name);
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

        // ���� ���� ���� ��������
        Color spriteColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        // ���ο� ���� �� ���� 
        float newAlpha = 0.5f;
        spriteColor.a = newAlpha;

        // ����� ���� ����
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = spriteColor;

        Debug.Log("��������Ʈ�� ���ο� ���� ��: " + transform.GetChild(0).GetComponent<SpriteRenderer>().color.a);

        animator.SetTrigger("IsDamage");

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
        isDead = true;

        // ���� ���� ���� ��������
        Color spriteColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        // ���ο� ���� �� ���� 
        float newAlpha = 1;
        spriteColor.a = newAlpha;

        // ����� ���� ����
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
