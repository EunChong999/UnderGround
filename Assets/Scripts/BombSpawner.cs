using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefeb;

    private SwitchGravity switchGravity;

    void Start()
    {
        switchGravity = GetComponent<SwitchGravity>();
    }

    void Update()
    {
        if (!switchGravity.isChangingGravity)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !switchGravity.IsGrounded(switchGravity.spaceCheck[0])) // Complete
            {
                if (transform.eulerAngles.z == 0)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = 1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (transform.eulerAngles.z == 90)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = -1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (transform.eulerAngles.z == 180)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = -1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.eulerAngles.z == 270)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 90);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && !switchGravity.IsGrounded(switchGravity.spaceCheck[1]))
            {
                if (transform.eulerAngles.z == 0)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = -1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (transform.eulerAngles.z == 90)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = -1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.eulerAngles.z == 180)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (transform.eulerAngles.z == 270)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = 1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 180);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !switchGravity.IsGrounded(switchGravity.spaceCheck[2]))
            {
                if (transform.eulerAngles.z == 0)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (transform.eulerAngles.z == 90)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = 1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (transform.eulerAngles.z == 180)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = -1;
                    bomb.GetComponent<GridMovement>().y = 0;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 270);
                }
                else if (transform.eulerAngles.z == 270)
                {
                    GameObject bomb = Instantiate(bombPrefeb, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
                    bomb.GetComponent<GridMovement>().x = 0;
                    bomb.GetComponent<GridMovement>().y = -1;
                    bomb.transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
        }
    }
}
