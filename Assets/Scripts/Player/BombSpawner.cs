using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefeb;
    [SerializeField] private GameObject bombInHand;

    private SwitchGravity switchGravity;
    private Health health;
    private Animator animator;

    void Start()
    {
        health = GetComponent<Health>();
        switchGravity = GetComponent<SwitchGravity>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (!health.isDead)
        {
            if (!switchGravity.isMoving)
            {
                if (Input.GetKey(KeyCode.UpArrow) && switchGravity.isSpaced[0])
                {
                    animator.SetBool("isAttacking", true);
                    animator.SetFloat("horizontal", 0);
                    animator.SetFloat("vertical", 1);
                    bombInHand.SetActive(false);
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && switchGravity.isSpaced[1])
                {
                    animator.SetBool("isAttacking", true);
                    animator.SetFloat("horizontal", -1);
                    animator.SetFloat("vertical", 0);
                    bombInHand.SetActive(true);
                    bombInHand.transform.localPosition = new Vector2(-0.15f, -0.25f);
                }
                else if (Input.GetKey(KeyCode.DownArrow) && switchGravity.isSpaced[2])
                {
                    animator.SetBool("isAttacking", true);
                    animator.SetFloat("horizontal", 0);
                    animator.SetFloat("vertical", -1);
                    bombInHand.SetActive(true);
                    bombInHand.transform.localPosition = new Vector2(-0.15f, -0.25f);
                }
                else if (Input.GetKey(KeyCode.RightArrow) && switchGravity.isSpaced[3])
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

                if (Input.GetKeyUp(KeyCode.UpArrow) && switchGravity.isSpaced[0])
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y + 0.25f), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = 1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (Input.GetKeyUp(KeyCode.LeftArrow) && switchGravity.isSpaced[1])
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x - 0.25f, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = -1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow) && switchGravity.isSpaced[2])
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y - 0.25f), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = -1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (Input.GetKeyUp(KeyCode.RightArrow) && switchGravity.isSpaced[3])
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
