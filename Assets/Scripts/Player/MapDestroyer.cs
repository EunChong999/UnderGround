using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class MapDestroyer : MonoBehaviour
{
    [SerializeField] private Tilemap gamePlayTilemap;
    [SerializeField] private Tilemap objectTilemap;
    [SerializeField] private RuleTile wallTile;
    [SerializeField] private Tile destructibleTile;
    [SerializeField] private GameObject explosionPrefeb;
    [SerializeField] private int explosionRange;

    [SerializeField] private AstarPath astar;

    public Bounds updateBounds;

    public void Explode(Transform worldPos)
    {
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
