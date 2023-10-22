using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefeb;
    [SerializeField] private GameObject bombInHand;

    private UndergroundMovement undergroundMovement;
    private Health health;
    private Animator animator;

    void Start()
    {
        health = GetComponent<Health>();
        undergroundMovement = GetComponent<UndergroundMovement>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (!health.isDead)
        {
            if (undergroundMovement.isReached)
            {
                if (Input.GetKey(KeyCode.UpArrow) && (undergroundMovement.isSpaced[0] || !undergroundMovement.isMoveStart))
                {
                    animator.SetBool("isAttacking", true);
                    animator.SetFloat("horizontal", 0);
                    animator.SetFloat("vertical", 1);
                    bombInHand.SetActive(false);
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && (undergroundMovement.isSpaced[1] || !undergroundMovement.isMoveStart))
                {
                    animator.SetBool("isAttacking", true);
                    animator.SetFloat("horizontal", -1);
                    animator.SetFloat("vertical", 0);
                    bombInHand.SetActive(true);
                    bombInHand.transform.localPosition = new Vector2(-0.15f, -0.25f);
                }
                else if (Input.GetKey(KeyCode.DownArrow) && (undergroundMovement.isSpaced[2] || !undergroundMovement.isMoveStart))
                {
                    animator.SetBool("isAttacking", true);
                    animator.SetFloat("horizontal", 0);
                    animator.SetFloat("vertical", -1);
                    bombInHand.SetActive(true);
                    bombInHand.transform.localPosition = new Vector2(-0.15f, -0.25f);
                }
                else if (Input.GetKey(KeyCode.RightArrow) && (undergroundMovement.isSpaced[3] || !undergroundMovement.isMoveStart))
                {
                    animator.SetBool("isAttacking", true);
                    animator.SetFloat("horizontal", 1);
                    animator.SetFloat("vertical", 0);
                    bombInHand.SetActive(true);
                    bombInHand.transform.localPosition = new Vector2(0.15f, -0.25f);
                }
                else
                {
                    animator.SetBool("isAttacking", false);
                    bombInHand.SetActive(false);
                }

                if (Input.GetKeyUp(KeyCode.UpArrow) && (undergroundMovement.isSpaced[0] || !undergroundMovement.isMoveStart))
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y + 0.25f), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = 1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (Input.GetKeyUp(KeyCode.LeftArrow) && (undergroundMovement.isSpaced[1] || !undergroundMovement.isMoveStart))
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x - 0.25f, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = -1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow) && (undergroundMovement.isSpaced[2] || !undergroundMovement.isMoveStart))
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y - 0.25f), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = -1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (Input.GetKeyUp(KeyCode.RightArrow) && (undergroundMovement.isSpaced[3] || !undergroundMovement.isMoveStart))
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x + 0.25f, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 90);
                }
            }
            else
            {
                animator.SetBool("isAttacking", false);
                bombInHand.SetActive(false);
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
            bombInHand.SetActive(false);
        }
    }
}
