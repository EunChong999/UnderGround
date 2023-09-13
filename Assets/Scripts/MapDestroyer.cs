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

    public void Explode(Transform worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos.position);

        ExplodeCell(worldPos, originCell);

        if (ExplodeCell(worldPos, originCell + new Vector3Int(1, 0, 0)))
        {
            ExplodeCell(worldPos, originCell + new Vector3Int(2, 0, 0));
        }

        if (ExplodeCell(worldPos, originCell + new Vector3Int(0, 1, 0)))
        {
            ExplodeCell(worldPos, originCell + new Vector3Int(0, 2, 0));
        }

        if (ExplodeCell(worldPos, originCell + new Vector3Int(-1, 0, 0)))
        {
            ExplodeCell(worldPos, originCell + new Vector3Int(-2, 0, 0));
        }

        if (ExplodeCell(worldPos, originCell + new Vector3Int(0, -1, 0)))
        {
            ExplodeCell(worldPos, originCell + new Vector3Int(0, -2, 0));
        }
    }

    bool ExplodeCell(Transform worldPos, Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);
        Vector3 pos = tilemap.GetCellCenterWorld(cell);

        if (tile == wallTile)
        {
            return false;
        }

        if (tile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
            Instantiate(explosionPrefeb, pos, worldPos.rotation);
            return true;
        }

        Instantiate(explosionPrefeb, pos, worldPos.rotation);

        return false;
    }
}
