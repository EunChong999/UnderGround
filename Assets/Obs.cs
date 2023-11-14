using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obs : MonoBehaviour
{
    [SerializeField]
    private GameObject throwPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Explosion"))
        {
            throwPoint.GetComponent<TutorialPoint>().isPointCleared = true;
        }
    }
}
