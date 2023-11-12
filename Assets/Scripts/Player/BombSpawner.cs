using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefeb;
    [SerializeField] private GameObject bombInHand;
    [SerializeField] private bool isGetKey;
    [SerializeField] private bool[] isKeysGetKey;

    private UndergroundMovement undergroundMovement;
    private PlayerHealth playerHealth;
    private Animator animator;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        undergroundMovement = GetComponent<UndergroundMovement>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (!playerHealth.isDead)
        {
            if (undergroundMovement.isReached)
            {
                // 방향 선택
                SelectDirection();

                // 방향 바라보기
                LookDirection();

                // 폭탄 던지기
                ThrowBomb();
            }
            else
            {
                animator.SetBool("IsAttacking", false);
                bombInHand.SetActive(false);
            }
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            bombInHand.SetActive(false);
        }
    }

    private void SelectDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && (undergroundMovement.isSpaced[0] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[0]))
        {
            isKeysGetKey[0] = true;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && (undergroundMovement.isSpaced[1] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[1]))
        {
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = true;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (undergroundMovement.isSpaced[2] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[2]))
        {
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = true;
            isKeysGetKey[3] = false;
            isGetKey = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && (undergroundMovement.isSpaced[3] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[3]))
        {
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = true;
            isGetKey = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && isKeysGetKey[0])
        {
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && isKeysGetKey[1])
        {
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = false;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && isKeysGetKey[2])
        {
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = false;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && isKeysGetKey[3])
        {
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = false;
        }
    }

    private void LookDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow) && (undergroundMovement.isSpaced[0] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[0]))
        {
            animator.SetBool("IsAttacking", true);
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 1);
            bombInHand.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && (undergroundMovement.isSpaced[1] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[1]))
        {
            animator.SetBool("IsAttacking", true);
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);
            bombInHand.SetActive(true);
            bombInHand.transform.localPosition = new Vector2(-0.15f, -0.25f);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && (undergroundMovement.isSpaced[2] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[2]))
        {
            animator.SetBool("IsAttacking", true);
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", -1);
            bombInHand.SetActive(true);
            bombInHand.transform.localPosition = new Vector2(-0.15f, -0.25f);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && (undergroundMovement.isSpaced[3] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[3]))
        {
            animator.SetBool("IsAttacking", true);
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);
            bombInHand.SetActive(true);
            bombInHand.transform.localPosition = new Vector2(0.15f, -0.25f);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            bombInHand.SetActive(false);
        }
    }

    private void ThrowBomb()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && (undergroundMovement.isSpaced[0] || !undergroundMovement.isMoveStart) && isKeysGetKey[0])
        {
            GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y + 0.25f), transform.rotation);
            bomb.GetComponent<GridMovement>().x = 0;
            bomb.GetComponent<GridMovement>().y = 1;
            bomb.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && (undergroundMovement.isSpaced[1] || !undergroundMovement.isMoveStart) && isKeysGetKey[1])
        {
            GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x - 0.25f, transform.position.y), transform.rotation);
            bomb.GetComponent<GridMovement>().x = -1;
            bomb.GetComponent<GridMovement>().y = 0;
            bomb.transform.eulerAngles = new Vector3(0, 0, 270);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && (undergroundMovement.isSpaced[2] || !undergroundMovement.isMoveStart) && isKeysGetKey[2])
        {
            GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y - 0.25f), transform.rotation);
            bomb.GetComponent<GridMovement>().x = 0;
            bomb.GetComponent<GridMovement>().y = -1;
            bomb.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && (undergroundMovement.isSpaced[3] || !undergroundMovement.isMoveStart) && isKeysGetKey[3])
        {
            GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x + 0.25f, transform.position.y), transform.rotation);
            bomb.GetComponent<GridMovement>().x = 1;
            bomb.GetComponent<GridMovement>().y = 0;
            bomb.transform.eulerAngles = new Vector3(0, 0, 90);
        }
    }
}
