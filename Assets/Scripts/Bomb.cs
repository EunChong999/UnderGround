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
            FindObjectOfType<MapDestroyer>().Explode(transform);
            Destroy(gameObject);
        }
    }
}
