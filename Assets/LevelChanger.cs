using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public static bool isLoading;

    private Animator animator;
    private string targetSceneName;
    private Image image;

    private void Awake()
    {
        isLoading = true;
    }

    private void Start()
    {
        image = GameObject.Find("Black Fade").GetComponent<Image>();    
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isLoading)
        {
            image.raycastTarget = true;
            image.maskable = true;
        }
        else
        {
            image.raycastTarget = false;
            image.maskable = false;
        }

        if (SceneManager.GetActiveScene().name == "Game Title Scene" && !isLoading)
        {
            if (Input.anyKeyDown)
            {
                LoadGameStageScene();
            }
        }

        if (SceneManager.GetActiveScene().name == "Game Play Scene" && !isLoading)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadGamePlayScene();
            }
        }
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(targetSceneName);
    }

    public void OnFadeInComplete()
    {
        isLoading = false;
    }
    
    private void LoadGameTitleScene()
    {
        targetSceneName = "Game Title Scene";
        animator.SetTrigger("FadeOut");
    }

    private void LoadGameStageScene()
    {
        targetSceneName = "Game Stage Scene";
        animator.SetTrigger("FadeOut");
    }

    private void LoadGamePlayScene()
    {
        targetSceneName = "Game Play Scene";
        animator.SetTrigger("FadeOut");
    }
}
