using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject backGround;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        backGround.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        backGround.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        player.SetActive(false);
        pauseMenu.SetActive(false);
        backGround.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        player.SetActive(false);
        pauseMenu.SetActive(false);
        backGround.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
