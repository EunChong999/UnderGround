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

    [SerializeField] private AstarPath astar;

    public Bounds updateBounds;

    // Bounds를 시각화하는 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 시각화 색상 설정
        Gizmos.DrawWireCube(updateBounds.center, updateBounds.size); // Bounds를 시각화
    }

    public void Explode(Transform worldPos)
    {
        Vector3Int originCell = gamePlayTilemap.WorldToCell(worldPos.position);

        ExplodeCellCheck(worldPos, originCell);

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i + 1, 0)))
            {
                ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i + 2, 0));
            }
            else
            {
                Debug.Log("Up Explosion Size : " + Mathf.Abs(i));
                //StartCoroutine(ExplodeCell(Mathf.Abs(i), worldPos, originCell));
                break;
            }
        }

        for (int i = 0; i < explosionRange; i--)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(i - 1, 0, 0)))
            {
                ExplodeCellCheck(worldPos, originCell + new Vector3Int(i - 2, 0, 0));
            }
            else
            {
                Debug.Log("Left Explosion Size : " + Mathf.Abs(i));
                //StartCoroutine(ExplodeCell(Mathf.Abs(i), worldPos, originCell));
                break;
            }
        }

        for (int i = 0; i < explosionRange; i--)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i - 1, 0)))
            {
                ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i - 2, 0));
            }
            else
            {
                Debug.Log("Down Explosion Size : " + Mathf.Abs(i));
                //StartCoroutine(ExplodeCell(Mathf.Abs(i), worldPos, originCell));
                break;
            }
        }

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(i + 1, 0, 0)))
            {
                ExplodeCellCheck(worldPos, originCell + new Vector3Int(i + 2, 0, 0));
            }
            else
            {
                Debug.Log("Right Explosion Size : " + Mathf.Abs(i));
                //StartCoroutine(ExplodeCell(Mathf.Abs(i), worldPos, originCell));
                break;
            }
        }
    }

    bool ExplodeCellCheck(Transform worldPos, Vector3Int cell)
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

            AstarPath.active.UpdateGraphs(updateBounds, 0);
        }

        Vector3 pos = gamePlayTilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefeb, pos, worldPos.rotation);

        return true;
    }

    private IEnumerator ExplodeCell(int explosionSize, Transform worldPos, Vector3Int originCell) // 받은 거리 값만큼 월드 포스를 기준으로 하여 정해진 방향으로 순차적으로 터뜨리기
    {
        while (true)
        {
            for (int i = 0; i < explosionSize; i++)
            {
                yield return new WaitForSeconds(interval);

                if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i + 1, 0)))
                {
                    ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i + 2, 0));
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < explosionSize; i--)
            {
                if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(i - 1, 0, 0)))
                {
                    ExplodeCellCheck(worldPos, originCell + new Vector3Int(i - 2, 0, 0));
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < explosionSize; i--)
            {
                if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i - 1, 0)))
                {
                    ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i - 2, 0));
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < explosionSize; i++)
            {
                if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(i + 1, 0, 0)))
                {
                    ExplodeCellCheck(worldPos, originCell + new Vector3Int(i + 2, 0, 0));
                }
                else
                {
                    break;
                }
            }

            yield break;
        }
    }
}
