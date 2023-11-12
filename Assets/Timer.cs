using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    float remainingTime;

    PlayerHealth health;

    private void Start()
    {
        health = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        
        if (remainingTime <= 0) 
        {
            remainingTime = 0;
            timerText.color = Color.red;
            health.Dead();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
