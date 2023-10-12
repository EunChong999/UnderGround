using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private float spawnInterval = 0.5f; // ���� ����
    private Vector3 currentSpawnPosition; // ���� ���� ��ġ
    private Vector3 targetSpawnPosition;  // ��ǥ ���� ��ġ

    private void Start()
    {
        currentSpawnPosition = transform.position; // ���� ��ġ
        targetSpawnPosition = new Vector3(10f, -5f, 0f); // ��ǥ ��ġ
        StartCoroutine(SpawnObjectOnXIncrease());
    }

    IEnumerator SpawnObjectOnXIncrease()
    {
        while (currentSpawnPosition.x < targetSpawnPosition.x)
        {
            currentSpawnPosition.x += 0.5f; // x���� 0.5�� ����
            Instantiate(objectToSpawn, currentSpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
