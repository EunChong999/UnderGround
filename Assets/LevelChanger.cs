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
