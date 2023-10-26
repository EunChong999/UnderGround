using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;

    public bool isLoading;
    public bool isTutorialCompleted;
    public GameObject[] levels;

    private Animator animator;
    private string targetSceneName;
    private Image image;

    //[HideInInspector]
    public int currentLevel;

    private void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static LevelManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        isLoading = true;
        currentLevel = 1;
        image = GameObject.Find("Black Fade").GetComponent<Image>();    
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isLoading)
        {
            image.raycastTarget = true;
        }
        else
        {
            image.raycastTarget = false;
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

    public void SpawnLevel()
    {
        Instantiate(levels[currentLevel - 1]);
    }

    private void Fade()
    {
        isLoading = true;
        animator.SetTrigger("FadeOut");
    }
    
    public void LoadGameTitleScene()
    {
        Fade();
        targetSceneName = "Game Title Scene";
    }

    public void LoadGameStageScene()
    {
        if (!isTutorialCompleted)
        {
            Fade();
            targetSceneName = "Game Tutorial Scene";
            isTutorialCompleted = true;
        }
        else
        {
            Fade();
            targetSceneName = "Game Stage Scene";
        }
    }

    public void LoadGamePlayScene()
    {
        Fade();
        targetSceneName = "Game Play Scene";
    }
}
