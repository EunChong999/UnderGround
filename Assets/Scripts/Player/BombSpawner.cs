using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefeb;

    private SwitchGravity switchGravity;
    Health health;

    void Start()
    {
        health = GetComponent<Health>();
        switchGravity = GetComponent<SwitchGravity>();
    }

    void Update()
    {
        if (!health.isDead)
        {
            if (!switchGravity.isMoving)
            {
                if (Input.GetKeyUp(KeyCode.UpArrow) && switchGravity.isSpaced[0])
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = 1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (Input.GetKeyUp(KeyCode.LeftArrow) && switchGravity.isSpaced[1])
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = -1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow) && switchGravity.isSpaced[2])
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = -1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (Input.GetKeyUp(KeyCode.RightArrow) && switchGravity.isSpaced[3])
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 90);
                }
            }
        }
    }
}
