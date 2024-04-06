using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
using Unity.VisualScripting;

public class MapDestroyer : MonoBehaviour
{
    [SerializeField] private Tilemap gamePlayTilemap; // 게임 플레이에 사용되는 타일맵
    [SerializeField] private Tilemap objectTilemap; // 오브젝트를 배치하는 타일맵
    [SerializeField] private RuleTile wallTile; // 벽 타일
    [SerializeField] private Tile destructibleTile; // 파괴 가능한 타일
    [SerializeField] private GameObject explosionPrefeb; // 폭발 효과 프리팹
    [SerializeField] private int explosionRange; // 폭발 범위
    [SerializeField] private float interval; // 폭발 간격

    public Bounds updateBounds; // 업데이트 할 Bounds 영역

    // 폭발 효과와 관련된 객체들의 풀
    public Queue<GameObject> pooledUpObjects;
    public Queue<GameObject> pooledLeftObjects;
    public Queue<GameObject> pooledDownObjects;
    public Queue<GameObject> pooledRightObjects;

    // 폭발 효과와 관련된 타일들의 위치 풀
    public Queue<Vector3Int> pooledUpTilesPos;
    public Queue<Vector3Int> pooledLeftTilesPos;
    public Queue<Vector3Int> pooledDownTilesPos;
    public Queue<Vector3Int> pooledRightTilesPos;

    // Bounds를 시각화하는 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 시각화 색상 설정
        Gizmos.DrawWireCube(updateBounds.center, updateBounds.size); // Bounds를 시각화
    }

    private void Start()
    {
        AstarPath.active.UpdateGraphs(updateBounds, 0); // A* Pathfinding 그래프 업데이트
    }

    // 폭발 함수
    public void Explode(Transform worldPos)
    {
        // 초기화
        pooledUpObjects = new Queue<GameObject>();
        pooledLeftObjects = new Queue<GameObject>();
        pooledDownObjects = new Queue<GameObject>();
        pooledRightObjects = new Queue<GameObject>();
        pooledUpTilesPos = new Queue<Vector3Int>();
        pooledLeftTilesPos = new Queue<Vector3Int>();
        pooledDownTilesPos = new Queue<Vector3Int>();
        pooledRightTilesPos = new Queue<Vector3Int>();

        // 폭발 발생 위치의 셀 계산
        Vector3Int originCell = gamePlayTilemap.WorldToCell(worldPos.position);

        // 중앙 폭발 효과 생성
        ExplodeCellCheck(worldPos, originCell, "middle");

        // 상하좌우 폭발 효과 및 타일 파괴 생성
        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i + 1, 0), "up")) { }
            else
            {
                break;
            }
        }

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int((i * (-1)) - 1, 0, 0), "left")) { }
            else
            {
                break;
            }
        }

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, (i * (-1)) - 1, 0), "down")) { }
            else
            {
                break;
            }
        }

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(i + 1, 0, 0), "right")) { }
            else
            {
                break;
            }
        }

        // 폭발 효과 및 타일 파괴 코루틴 실행
        StartCoroutine(ExplodeCell(pooledUpObjects));
        StartCoroutine(ExplodeTile(pooledUpTilesPos));

        StartCoroutine(ExplodeCell(pooledLeftObjects));
        StartCoroutine(ExplodeTile(pooledLeftTilesPos));

        StartCoroutine(ExplodeCell(pooledDownObjects));
        StartCoroutine(ExplodeTile(pooledDownTilesPos));

        StartCoroutine(ExplodeCell(pooledRightObjects));
        StartCoroutine(ExplodeTile(pooledRightTilesPos));
    }

    // 폭발 효과 객체들을 활성화
    IEnumerator ExplodeCell(Queue<GameObject> pooledObjects)
    {
        while (pooledObjects.Count > 0)
        {
            yield return new WaitForSeconds(interval);
            pooledObjects.Dequeue().SetActive(true);
        }
        yield break;
    }

    // 폭발로 인해 파괴된 타일들 처리
    IEnumerator ExplodeTile(Queue<Vector3Int> pooledTilesPos)
    {
        while (pooledTilesPos.Count > 0)
        {
            yield return new WaitForSeconds(interval);
            objectTilemap.SetTile(pooledTilesPos.Dequeue(), null); // 파괴된 타일 제거
            AstarPath.active.UpdateGraphs(updateBounds, 0); // A* Pathfinding 그래프 업데이트
        }
    }

    // 폭발 효과 및 타일 파괴 체크 함수
    bool ExplodeCellCheck(Transform worldPos, Vector3Int originCell, string str)
    {
        RuleTile gamePlayRuleTile = gamePlayTilemap.GetTile<RuleTile>(originCell);

        if (gamePlayRuleTile == wallTile)
        {
            return false;
        }

        Vector3 pos = gamePlayTilemap.GetCellCenterWorld(originCell);

        // 폭발 효과 및 파괴된 타일 생성
        switch (str)
        {
            case "middle":
                Instantiate(explosionPrefeb, pos, worldPos.rotation);
                break;
            case "up":
                GameObject objUp = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objUp.SetActive(false);
                pooledUpObjects.Enqueue(objUp);
                pooledUpTilesPos.Enqueue(originCell);
                break;
            case "left":
                GameObject objLeft = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objLeft.SetActive(false);
                pooledLeftObjects.Enqueue(objLeft);
                pooledLeftTilesPos.Enqueue(originCell);
                break;
            case "down":
                GameObject objDown = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objDown.SetActive(false);
                pooledDownObjects.Enqueue(objDown);
                pooledDownTilesPos.Enqueue(originCell);
                break;
            case "right":
                GameObject objRight = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objRight.SetActive(false);
                pooledRightObjects.Enqueue(objRight);
                pooledRightTilesPos.Enqueue(originCell);
                break;
            default:
                break;
        }

        return true;
    }
}
