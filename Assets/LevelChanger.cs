using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;
    private Scene targetScene;

    [SerializeField]
    private bool isLoading;

    private void Awake()
    {
        isLoading = true;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isLoading)
        {
            LoadGamePlayScene();
        }
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(targetScene.name);
    }

    public void OnFadeInComplete()
    {
        isLoading = false;
    }
    
    private void LoadGameTitleScene()
    {
        targetScene = SceneManager.GetSceneByName("Game Title Scene");
        animator.SetTrigger("FadeOut");
    }

    private void LoadGameStageScene()
    {
        targetScene = SceneManager.GetSceneByName("Game Stage Scene");
        animator.SetTrigger("FadeOut");
    }

    private void LoadGamePlayScene()
    {
        targetScene = SceneManager.GetSceneByName("Game Play Scene");
        animator.SetTrigger("FadeOut");
    }
}
