using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Quaternion currentAngle;

    public void ChangeRotate(float rot)
    {
        currentAngle = Quaternion.Euler(0, 0, rot);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, currentAngle, 0.01f);
    }
}

