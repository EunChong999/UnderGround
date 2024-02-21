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

    public bool[] isLockedKey;

    private bool isBombSpawned;
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
            // 방향 선택
            SelectDirection();

            if (undergroundMovement.isReached)
            {
                if (!isBombSpawned)
                {
                    // 방향 바라보기
                    LookDirection();

                    // 폭탄 던지기
                    ThrowBomb();
                }
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isKeysGetKey[0] = true;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = true;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = true;
            isKeysGetKey[3] = false;
            isGetKey = true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = true;
            isGetKey = true;
        }
    }

    private void LookDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow) && (undergroundMovement.isSpaced[0] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[0]) && !isLockedKey[0])
        {
            animator.SetBool("IsAttacking", true);
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 1);
            bombInHand.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && (undergroundMovement.isSpaced[1] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[1]) && !isLockedKey[1])
        {
            animator.SetBool("IsAttacking", true);
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);
            bombInHand.SetActive(true);
            bombInHand.transform.localPosition = new Vector2(-0.15f, -0.25f);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && (undergroundMovement.isSpaced[2] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[2]) && !isLockedKey[2])
        {
            animator.SetBool("IsAttacking", true);
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", -1);
            bombInHand.SetActive(true);
            bombInHand.transform.localPosition = new Vector2(-0.15f, -0.25f);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && (undergroundMovement.isSpaced[3] || !undergroundMovement.isMoveStart) && (!isGetKey || isKeysGetKey[3]) && !isLockedKey[3])
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
        if (Input.GetKeyUp(KeyCode.UpArrow) && (undergroundMovement.isSpaced[0] || !undergroundMovement.isMoveStart) && isKeysGetKey[0] && !isLockedKey[0])
        {
            isBombSpawned = true;
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = false;
            GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y + 0.25f), transform.rotation);
            bomb.GetComponent<GridMovement>().x = 0;
            bomb.GetComponent<GridMovement>().y = 1;
            bomb.transform.eulerAngles = new Vector3(0, 0, 180);
            Invoke(nameof(BombReset), 1.5f);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && (undergroundMovement.isSpaced[1] || !undergroundMovement.isMoveStart) && isKeysGetKey[1] && !isLockedKey[1])
        {
            isBombSpawned = true;
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = false;
            GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x - 0.25f, transform.position.y), transform.rotation);
            bomb.GetComponent<GridMovement>().x = -1;
            bomb.GetComponent<GridMovement>().y = 0;
            bomb.transform.eulerAngles = new Vector3(0, 0, 270);
            Invoke(nameof(BombReset), 1.5f);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && (undergroundMovement.isSpaced[2] || !undergroundMovement.isMoveStart) && isKeysGetKey[2] && !isLockedKey[2])
        {
            isBombSpawned = true;
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = false;
            GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y - 0.25f), transform.rotation);
            bomb.GetComponent<GridMovement>().x = 0;
            bomb.GetComponent<GridMovement>().y = -1;
            bomb.transform.eulerAngles = new Vector3(0, 0, 0);
            Invoke(nameof(BombReset), 1.5f);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && (undergroundMovement.isSpaced[3] || !undergroundMovement.isMoveStart) && isKeysGetKey[3] && !isLockedKey[3])
        {
            isBombSpawned = true;
            isKeysGetKey[0] = false;
            isKeysGetKey[1] = false;
            isKeysGetKey[2] = false;
            isKeysGetKey[3] = false;
            isGetKey = false;
            GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x + 0.25f, transform.position.y), transform.rotation);
            bomb.GetComponent<GridMovement>().x = 1;
            bomb.GetComponent<GridMovement>().y = 0;
            bomb.transform.eulerAngles = new Vector3(0, 0, 90);
            Invoke(nameof(BombReset), 1.5f);
        }
    }

    private void BombReset()
    {
        isBombSpawned = false;
    }
}
