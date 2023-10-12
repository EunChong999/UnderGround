using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private float spawnInterval = 0.5f; // 생성 간격
    private Vector3 currentSpawnPosition; // 현재 생성 위치
    private Vector3 targetSpawnPosition;  // 목표 생성 위치

    private void Start()
    {
        currentSpawnPosition = transform.position; // 시작 위치
        targetSpawnPosition = new Vector3(10f, -5f, 0f); // 목표 위치
        StartCoroutine(SpawnObjectOnXIncrease());
    }

    IEnumerator SpawnObjectOnXIncrease()
    {
        while (currentSpawnPosition.x < targetSpawnPosition.x)
        {
            currentSpawnPosition.x += 0.5f; // x값을 0.5씩 증가
            Instantiate(objectToSpawn, currentSpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
