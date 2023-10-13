using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] float countdown;
    private GridMovement gridMovement;

    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.4f, wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(wallCheck.position, 0.25f);
    }

    private void Start()
    {
        gridMovement = GetComponent<GridMovement>(); 
    }

    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 || IsWalled())
        {
            FindObjectOfType<MapDestroyer>().Explode(transform);
            Destroy(gameObject);
        }
    }
}
