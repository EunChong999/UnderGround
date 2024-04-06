using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefeb;

    public float bulletForce;
    public float distance;
    private GameObject player;
    [SerializeField]
    private LayerMask obsLayer;

    RaycastHit2D raycastHit2Dright;
    RaycastHit2D raycastHit2Dleft;
    RaycastHit2D raycastHit2Dup;
    RaycastHit2D raycastHit2Ddown;

    BasicEnemy enemyHealth;

    private void Start()
    {
        enemyHealth = GetComponent<BasicEnemy>();

        enemyHealth.Init();

        player = GameObject.Find("Player");
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        enemyHealth.ManageHealth();

        FlipX();
        CheckPoint();
    }

    private void FlipX()
    {
        transform.rotation = player.transform.rotation;

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void CheckPoint()
    {
        raycastHit2Dright = Physics2D.Raycast(transform.position, Vector2.right, distance, obsLayer);
        raycastHit2Dleft = Physics2D.Raycast(transform.position, Vector2.left, distance, obsLayer);
        raycastHit2Dup = Physics2D.Raycast(transform.position, Vector2.up, distance, obsLayer);
        raycastHit2Ddown = Physics2D.Raycast(transform.position, Vector2.down, distance, obsLayer);
        Debug.DrawRay(transform.position, Vector2.right * raycastHit2Dright.distance, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * raycastHit2Dleft.distance, Color.red);
        Debug.DrawRay(transform.position, Vector2.up * raycastHit2Dup.distance, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * raycastHit2Ddown.distance, Color.red);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            GameObject bullet;
            Rigidbody2D rb;

            if (enemyHealth.isStartMove)
            {
                if (!enemyHealth.isDead)
                {
                    if (raycastHit2Dleft && raycastHit2Dleft.transform.gameObject.CompareTag("Player"))
                    {
                        bullet = Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);
                        rb = bullet.GetComponent<Rigidbody2D>();
                        rb.AddForce(-firePoint.right * bulletForce, ForceMode2D.Impulse);
                    }
                    else if (raycastHit2Dright && raycastHit2Dright.transform.gameObject.CompareTag("Player"))
                    {
                        bullet = Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);
                        rb = bullet.GetComponent<Rigidbody2D>();
                        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
                    }
                    else if (raycastHit2Ddown && raycastHit2Ddown.transform.gameObject.CompareTag("Player"))
                    {
                        bullet = Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);
                        rb = bullet.GetComponent<Rigidbody2D>();
                        rb.AddForce(-firePoint.up * bulletForce, ForceMode2D.Impulse);
                    }
                    else if (raycastHit2Dup && raycastHit2Dup.transform.gameObject.CompareTag("Player"))
                    {
                        bullet = Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);
                        rb = bullet.GetComponent<Rigidbody2D>();
                        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                    }
                }
            }

            yield return new WaitForSeconds(1.5f);
        }
    }
}
