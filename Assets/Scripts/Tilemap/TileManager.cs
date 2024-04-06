using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    // 타일맵 변수들을 선언합니다.
    public Tilemap BGTilemap;  // 배경 타일맵 (이동 가능 구역)
    public Tilemap GPTilemap;  // 게임 플레이 타일맵 (파괴 불가 구역)
    public Tilemap OBJTilemap; // 객체 타일맵 (파괴 가능 구역)

    // 적용할 RuleTile을 설정합니다.
    public RuleTile ruleTile;

    // 배경 타일맵의 바운딩 영역을 정의합니다.
    BoundsInt boundsBG;

    // 제거된 타일의 위치를 저장할 리스트를 선언합니다.
    List<Vector3Int> vector3Ints = new List<Vector3Int>();

    void Start()
    {
        // 배경 타일맵의 바운딩 영역을 설정합니다.
        boundsBG = BGTilemap.cellBounds;

        // 모든 타일 위치에 대해 반복합니다.
        foreach (var pos in boundsBG.allPositionsWithin)
        {
            // 객체 타일맵과 배경 타일맵에 모두 타일이 존재할 경우
            if (OBJTilemap.HasTile(pos) && BGTilemap.HasTile(pos))
            {
                // 배경 타일맵에서 타일을 제거하고, 제거된 위치를 리스트에 추가합니다.
                BGTilemap.SetTile(pos, null);
                vector3Ints.Add(pos);
            }

            // GP 타일맵과 배경 타일맵에 모두 타일이 존재할 경우
            if (GPTilemap.HasTile(pos) && BGTilemap.HasTile(pos))
            {
                // 배경 타일맵에서 타일을 제거합니다.
                BGTilemap.SetTile(pos, null);
            }
        }
    }

    private void Update()
    {
        // 제거된 타일의 위치에 대해 반복합니다.
        foreach (var pos in vector3Ints)
        {
            // 해당 위치에 객체 타일과 배경 타일이 모두 없을 경우
            if (!OBJTilemap.HasTile(pos) && !BGTilemap.HasTile(pos))
            {
                // 배경 타일맵에 RuleTile을 적용하여 원래 상태로 되돌립니다.
                BGTilemap.SetTile(pos, ruleTile);
            }
        }
    }
}
