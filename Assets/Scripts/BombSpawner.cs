using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject bombPrefeb;

    private SwitchGravity switchGravity;

    // Start is called before the first frame update
    void Start()
    {
        switchGravity = GetComponent<SwitchGravity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !switchGravity.isChangingGravity)
        {
            Instantiate(bombPrefeb, transform.position, transform.rotation);
        }
    }
}
