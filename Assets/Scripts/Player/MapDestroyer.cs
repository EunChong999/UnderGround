using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private RuleTile wallTile;
    [SerializeField] private Tile destructibleTile;
    [SerializeField] private GameObject explosionPrefeb;
    [SerializeField] private int explosionRange;

    public void Explode(Transform worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos.position);

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
        Tile tile = tilemap.GetTile<Tile>(cell);
        RuleTile ruleTile = tilemap.GetTile<RuleTile>(cell);

        if (tile == wallTile || ruleTile == wallTile)
        {
            return false;
        }

        if (tile == destructibleTile || ruleTile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
        }

        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefeb, pos, worldPos.rotation);

        return true;
    }
}
