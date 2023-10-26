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
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
            //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameMgr�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameMgr)�� �������ش�.
            Destroy(this.gameObject);
        }
    }

    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
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
