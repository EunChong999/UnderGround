using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefeb;

    public float bulletForce;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            GameObject bullet;
            Rigidbody2D rb;

            if (transform.position.x > player.transform.position.x && Mathf.Abs(transform.position.y - player.transform.position.y) < 0.5f)
            {
                bullet = Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);
                rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(-firePoint.right * bulletForce, ForceMode2D.Impulse);
            }
            else if (transform.position.x < player.transform.position.x && Mathf.Abs(transform.position.y - player.transform.position.y) < 0.5f)
            {
                bullet = Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);
                rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            }
            else if (transform.position.y > player.transform.position.y && Mathf.Abs(transform.position.x - player.transform.position.x) < 0.5f)
            {
                bullet = Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);
                rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(-firePoint.up * bulletForce, ForceMode2D.Impulse);
            }
            else if (transform.position.y < player.transform.position.y && Mathf.Abs(transform.position.x - player.transform.position.x) < 0.5f)
            {
                bullet = Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);
                rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(1);
        }
    }
}
