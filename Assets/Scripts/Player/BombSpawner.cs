using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefeb;

    Health health;
    private SwitchGravity switchGravity;
    Animator animator;

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
            if (!switchGravity.isChangingGravity)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow)) // Complete
                {
                    switchGravity.direction = switchGravity.spaceCheck[0];

                    animator.SetFloat("horizontal", 0);
                    animator.SetFloat("vertical", 1);

                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = 1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    switchGravity.direction = switchGravity.spaceCheck[1];

                    animator.SetFloat("horizontal", -1);
                    animator.SetFloat("vertical", 0);

                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = -1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    switchGravity.direction = switchGravity.spaceCheck[2];

                    animator.SetFloat("horizontal", 0);
                    animator.SetFloat("vertical", -1);

                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = -1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    switchGravity.direction = switchGravity.spaceCheck[3];

                    animator.SetFloat("horizontal", 1);
                    animator.SetFloat("vertical", 0);

                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 90);
                }
            }
        }
    }
}
