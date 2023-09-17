using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRotate : MonoBehaviour
{
    void Update()
    {
#if !UNITY_EDITOR
        transform.Rotate(0, 0, 9);
#else
        transform.Rotate(0, 0, 3);
#endif
    }
}
