using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    Quaternion currentAngle;

    [SerializeField] private float lerpTime;
    [SerializeField] private float currentTime;

    private void Start()
    {
#if !UNITY_EDITOR
    lerpTime = 1;
#else
    lerpTime = 10;
#endif

        currentTime = 0;
    }

    public void ChangeRotate(float rot)
    {
        currentTime = 0;
        currentAngle = Quaternion.Euler(0, 0, rot);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= lerpTime) 
        {
            currentTime = lerpTime;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, currentAngle, currentTime / lerpTime);
    }
}

