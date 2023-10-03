using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
