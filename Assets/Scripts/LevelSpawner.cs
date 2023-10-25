using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private void OnEnable()
    {
        LevelManager.Instance.SpawnLevel();
    }
}
