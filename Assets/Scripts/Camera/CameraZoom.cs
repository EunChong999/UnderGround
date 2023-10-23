using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CameraZoom : MonoBehaviour
{
    private new CinemachineVirtualCamera camera;

    private float zoom;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    [SerializeField] private float smoothTime;

    private float velocity;
    private UndergroundMovement undergroundMovement;
    private CinemachinePixelPerfect pixelPerfect;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game Play Scene")
        {
            camera = GetComponent<CinemachineVirtualCamera>();
            zoom = minZoom;
            undergroundMovement = GameObject.Find("Player").GetComponent<UndergroundMovement>();
            pixelPerfect = GetComponent<CinemachinePixelPerfect>();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game Play Scene")
        {
            camera.m_Lens.OrthographicSize = zoom;

            if (!undergroundMovement.isReached)
            {
                if (zoom < maxZoom - 0.0001f)
                {
                    pixelPerfect.enabled = false;
                    zoom = Mathf.SmoothDamp(zoom, maxZoom, ref velocity, smoothTime);
                }
                else
                {
                    pixelPerfect.enabled = true;
                    zoom = maxZoom;
                }
            }
            else
            {
                if (zoom > minZoom + 0.0001f)
                {
                    pixelPerfect.enabled = false;
                    zoom = Mathf.SmoothDamp(zoom, minZoom, ref velocity, smoothTime);
                }
                else
                {
                    pixelPerfect.enabled = true;
                    zoom = minZoom;
                }
            }
        }
    }
}
