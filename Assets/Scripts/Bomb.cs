using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float countdown;

    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            //for (int i = 0; i < 4; i++) 
            //{

            //}
            FindObjectOfType<MapDestroyer>().Explode(transform.position);
            Destroy(gameObject);
        }
    }
}
