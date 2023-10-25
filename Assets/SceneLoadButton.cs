using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadButton : ButtonAnimation
{
    [SerializeField]
    private string loadSceneName;

    private void Start()
    {
        button.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        switch(loadSceneName)
        {
            case "Game Title Scene":
                LevelChanger.Instance.LoadGameTitleScene();
                break;
            case "Game Stage Scene":
                LevelChanger.Instance.LoadGameStageScene();
                break;
            case "Game Play Scene":
                LevelChanger.Instance.LoadGamePlayScene();
                break;
            default:
                break;
        }
    }
}
