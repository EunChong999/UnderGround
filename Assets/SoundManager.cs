using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject regular;
    [SerializeField]
    private GameObject battle;

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game Play Scene")
        {
            regular.SetActive(false);
            battle.SetActive(true);
        }
        else
        {
            regular.SetActive(true);
            battle.SetActive(false);
        }
    }
}
