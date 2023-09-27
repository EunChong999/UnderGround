using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
using Unity.VisualScripting;

public class MapDestroyer : MonoBehaviour
{
    [SerializeField] private Tilemap gamePlayTilemap;
    [SerializeField] private Tilemap objectTilemap;
    [SerializeField] private RuleTile wallTile;
    [SerializeField] private Tile destructibleTile;
    [SerializeField] private GameObject explosionPrefeb;
    [SerializeField] private int explosionRange;
    [SerializeField] private int interval;
    [SerializeField] private GameObject player;

    [SerializeField] private AstarPath astar;

    public Bounds updateBounds;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Bounds를 시각화하는 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 시각화 색상 설정
        Gizmos.DrawWireCube(updateBounds.center, updateBounds.size); // Bounds를 시각화
    }

    private IEnumerator ExplodeUp(int explosionSize, Transform worldPos, Vector3Int cell) // 받은 거리 값만큼 월드 포스를 기준으로 하여 정해진 방향으로 순차적으로 터뜨리기
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            ExplodeCellUp(explosionSize, worldPos, cell);
        }
    }

    private void ExplodeCellUp(int explosionSize, Transform worldPos, Vector3Int cell)
    {
        Tile objectTile = objectTilemap.GetTile<Tile>(cell);
        RuleTile objectRuleTile = objectTilemap.GetTile<RuleTile>(cell);

        if (objectTile == destructibleTile || objectRuleTile == destructibleTile)
        {
            objectTilemap.SetTile(cell, null);

            // 주어진 bounds와 delay로 그래프 업데이트 예약
            AstarPath.active.UpdateGraphs(updateBounds, 0);
        }

        Vector3 pos = gamePlayTilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefeb, pos, worldPos.rotation);
    }

    public void Explode(Transform worldPos)
    {
        // 플레이어의 현재 위치 저장
        Vector3Int playerPos = Vector3Int.zero;
        playerPos.y = (int)player.transform.position.y;

        Vector3Int originCell = gamePlayTilemap.WorldToCell(worldPos.position);

        ExplodeCell(worldPos, originCell);

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCell(worldPos, originCell + new Vector3Int(i + 1, 0, 0)))
            {
                ExplodeCell(worldPos, originCell + new Vector3Int(i + 2, 0, 0));
            }
            else
            {
                int explosionSize = Mathf.Abs(playerPos.y - (int)worldPos.position.y);
                Debug.Log(explosionSize);
                // 플레이어의 위치와 폭탄의 위치 사이의 거리 비교 
                // 해당 값을 코루틴의 매개변수로 등록
                // 지정된 시간 간격마다 호출될 함수를 Coroutine으로 실행합니다.
                //StartCoroutine(ExplodeUp(explosionSize, worldPos, originCell));
                break;
            }
        }

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCell(worldPos, originCell + new Vector3Int(0, i + 1, 0)))
            {
                ExplodeCell(worldPos, originCell + new Vector3Int(0, i + 2, 0));
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < explosionRange; i--)
        {
            if (ExplodeCell(worldPos, originCell + new Vector3Int(i - 1, 0, 0)))
            {
                ExplodeCell(worldPos, originCell + new Vector3Int(i - 2, 0, 0));
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < explosionRange; i--)
        {
            if (ExplodeCell(worldPos, originCell + new Vector3Int(0, i - 1, 0)))
            {
                ExplodeCell(worldPos, originCell + new Vector3Int(0, i - 2, 0));
            }
            else
            {
                break;
            }
        }
    }

    bool ExplodeCell(Transform worldPos, Vector3Int cell)
    {
        Tile gamePlayTile = gamePlayTilemap.GetTile<Tile>(cell);
        RuleTile gamePlayRuleTile = gamePlayTilemap.GetTile<RuleTile>(cell);
        Tile objectTile = objectTilemap.GetTile<Tile>(cell);
        RuleTile objectRuleTile = objectTilemap.GetTile<RuleTile>(cell);

        if (gamePlayTile == wallTile || gamePlayRuleTile == wallTile)
        {
            return false;
        }

        if (objectTile == destructibleTile || objectRuleTile == destructibleTile) 
        {
            objectTilemap.SetTile(cell, null);

            // 주어진 bounds와 delay로 그래프 업데이트 예약
            AstarPath.active.UpdateGraphs(updateBounds, 0);
        }

        Vector3 pos = gamePlayTilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefeb, pos, worldPos.rotation);

        return true;
    }
}
