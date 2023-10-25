using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ButtonSceneLoad : ButtonAnimation
{
    [SerializeField]
    private string loadSceneName;

    private void Start()
    {
        button.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        if (gameObject.name.Contains("Level"))
        {
            SendLevelInfo();

            if (LevelManager.Instance.levels[LevelManager.Instance.currentLevel - 1] != null)
            {
                LoadScene();
            }
        }
        else
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        switch(loadSceneName)
        {
            case "Game Title Scene":
                LevelManager.Instance.LoadGameTitleScene();
                break;
            case "Game Stage Scene":
                LevelManager.Instance.LoadGameStageScene();
                break;
            case "Game Play Scene":
                LevelManager.Instance.LoadGamePlayScene();
                break;
            default:
                break;
        }
    }

    private void SendLevelInfo()
    {
        LevelManager.Instance.currentLevel = int.Parse(new string(gameObject.name.Where(char.IsDigit).ToArray()));
    }
}
