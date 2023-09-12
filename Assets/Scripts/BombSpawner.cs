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
        if (Input.GetKeyDown(KeyCode.Space) && !switchGravity.isChangingGravity)
        {
            Instantiate(bombPrefeb, transform.position, transform.rotation);
        }
    }
}
