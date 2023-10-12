using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DigUpGround : MonoBehaviour
{
    [SerializeField] private GameObject underGroundTunnel;
    [SerializeField] private GameObject underGroundSill;
    private List<Vector3> spawnedTunnels = new List<Vector3>();
    private List<Vector3> spawnedSills = new List<Vector3>();
    private SwitchGravity switchGravity;

    public Vector3 scaler = new Vector3(1, 1, 1);
    public bool isIn;

    [SerializeField] private GameObject lamp;
    [SerializeField] private GameObject tunnel;

    private void Start()
    {
        switchGravity = transform.parent.GetComponent<SwitchGravity>();
        //StartCoroutine(DigDown());
    }

    private void Update()
    {
        transform.localScale = scaler;

        if (isIn)
        {
            scaler.x = Mathf.Lerp(scaler.x, 0, .15f);
            scaler.y = Mathf.Lerp(scaler.y, 0, .15f);
        }
        else
        {
            scaler.x = Mathf.Lerp(scaler.x, 1, .15f);
            scaler.y = Mathf.Lerp(scaler.y, 1, .15f);
        }

        if (switchGravity.isMoving)
        {
            isIn = true;
            lamp.GetComponent<Light2D>().intensity = Mathf.Lerp(lamp.GetComponent<Light2D>().intensity, 0, .05f);
            tunnel.SetActive(false);
            DigDown();
        }
        else
        {
            isIn = false;
            lamp.GetComponent<Light2D>().intensity = Mathf.Lerp(lamp.GetComponent<Light2D>().intensity, 2, .05f);
            tunnel.SetActive(true);
            DigUp();
        }
    }

    public void DigDown()
    {

    }

    public void DigUp()
    {
        // 현재 위치와 모든 이전 위치 비교
        if (!spawnedTunnels.Contains(transform.position))
        {
            Debug.Log("DigUp" + "(" + transform.position.x + ", " + transform.position.y + ")");
            Instantiate(underGroundTunnel, transform.position, Quaternion.identity);
            spawnedTunnels.Add(transform.position);
        }
        else
        {
            Debug.Log("이미 터널이 해당 위치에 생성되었습니다.");
        }
    }
}
