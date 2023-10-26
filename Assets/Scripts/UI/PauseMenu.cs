using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject backGround;

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
        pauseMenu.SetActive(false);
        backGround.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        pauseMenu.SetActive(false);
        backGround.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
