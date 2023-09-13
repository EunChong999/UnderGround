using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    [SerializeField] private List<Tilemap> tilemap;
    [SerializeField] private RuleTile wallTile;
    [SerializeField] private List<Tile> destructibleTile;

    public void Explode(Vector2 worldPos)
    {
        List<Vector3Int> originCell = null;

        for (int i = 0; i < tilemap.Count; i++) 
        {
            originCell.Add(tilemap[i].WorldToCell(worldPos));
            ExplodeCell(originCell[i]);
        }
    }

    void ExplodeCell(Vector3Int cell)
    {
        for (int t = 0; t < tilemap.Count; t++) 
        {
            Tile tile = tilemap[t].GetTile<Tile>(cell);

            if (tile == wallTile)
            {
                return;
            }

            for (int j = 0; j < destructibleTile.Count; j++)
            {
                if (tile == destructibleTile[j])
                {
                    tilemap[t].SetTile(cell, null);
                }
            }
        }
    }
}
