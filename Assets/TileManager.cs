using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap BGTilemap;
    public Tilemap GPTilemap;
    public Tilemap OBJTilemap;
    public RuleTile ruleTile;
    BoundsInt boundsBG;
    List<Vector3Int> vector3Ints = new List<Vector3Int>();

    void Start()
    {
        boundsBG = BGTilemap.cellBounds;

        foreach (var pos in boundsBG.allPositionsWithin)
        {
            if (OBJTilemap.HasTile(pos) && BGTilemap.HasTile(pos))
            {
                BGTilemap.SetTile(pos, null);
                vector3Ints.Add(pos);
            }

            if (GPTilemap.HasTile(pos) && BGTilemap.HasTile(pos))
            {
                BGTilemap.SetTile(pos, null);
            }
        }
    }

    private void Update()
    {
        foreach (var pos in vector3Ints)
        {
            if (!OBJTilemap.HasTile(pos) && !BGTilemap.HasTile(pos))
            {
                BGTilemap.SetTile(pos, ruleTile);
            }
        }
    }
}
