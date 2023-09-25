using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraZoom : MonoBehaviour
{
    private new CinemachineVirtualCamera camera;

    private float zoom;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    private SwitchGravity switchGravity;
    private CinemachinePixelPerfect pixelPerfect;

    void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        zoom = minZoom;
        switchGravity = GameObject.Find("Player").GetComponent<SwitchGravity>();
        pixelPerfect = GetComponent<CinemachinePixelPerfect>();
    }

    void Update()
    {
        camera.m_Lens.OrthographicSize = zoom;

        if (switchGravity.isChangingGravity) 
        {
            if (zoom < maxZoom) 
            {
                zoom = Mathf.Lerp(zoom, maxZoom, 0.05f);
                pixelPerfect.enabled = false;
            }
            else
            {
                zoom = maxZoom;
                pixelPerfect.enabled = true;
            }
        }
        else
        {
            if (zoom > minZoom)
            {
                zoom = Mathf.Lerp(zoom, minZoom, 0.05f);
                pixelPerfect.enabled = false;
            }
            else
            {
                zoom = minZoom;
                pixelPerfect.enabled = true;
            }
        }
    }
}
