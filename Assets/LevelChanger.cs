using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    private static LevelChanger instance = null;

    public bool isLoading;

    private Animator animator;
    private string targetSceneName;
    private Image image;

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
    public static LevelChanger Instance
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

        //if (SceneManager.GetActiveScene().name == "Game Title Scene" && !isLoading)
        //{
        //    if (Input.anyKeyDown)
        //    {
        //        LoadGameStageScene();
        //    }
        //}

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
        Fade();
        targetSceneName = "Game Stage Scene";
    }

    public void LoadGamePlayScene()
    {
        Fade();
        targetSceneName = "Game Play Scene";
    }
}
