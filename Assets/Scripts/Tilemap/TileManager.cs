using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    // Ÿ�ϸ� �������� �����մϴ�.
    public Tilemap BGTilemap;  // ��� Ÿ�ϸ� (�̵� ���� ����)
    public Tilemap GPTilemap;  // ���� �÷��� Ÿ�ϸ� (�ı� �Ұ� ����)
    public Tilemap OBJTilemap; // ��ü Ÿ�ϸ� (�ı� ���� ����)

    // ������ RuleTile�� �����մϴ�.
    public RuleTile ruleTile;

    // ��� Ÿ�ϸ��� �ٿ�� ������ �����մϴ�.
    BoundsInt boundsBG;

    // ���ŵ� Ÿ���� ��ġ�� ������ ����Ʈ�� �����մϴ�.
    List<Vector3Int> vector3Ints = new List<Vector3Int>();

    void Start()
    {
        // ��� Ÿ�ϸ��� �ٿ�� ������ �����մϴ�.
        boundsBG = BGTilemap.cellBounds;

        // ��� Ÿ�� ��ġ�� ���� �ݺ��մϴ�.
        foreach (var pos in boundsBG.allPositionsWithin)
        {
            // ��ü Ÿ�ϸʰ� ��� Ÿ�ϸʿ� ��� Ÿ���� ������ ���
            if (OBJTilemap.HasTile(pos) && BGTilemap.HasTile(pos))
            {
                // ��� Ÿ�ϸʿ��� Ÿ���� �����ϰ�, ���ŵ� ��ġ�� ����Ʈ�� �߰��մϴ�.
                BGTilemap.SetTile(pos, null);
                vector3Ints.Add(pos);
            }

            // GP Ÿ�ϸʰ� ��� Ÿ�ϸʿ� ��� Ÿ���� ������ ���
            if (GPTilemap.HasTile(pos) && BGTilemap.HasTile(pos))
            {
                // ��� Ÿ�ϸʿ��� Ÿ���� �����մϴ�.
                BGTilemap.SetTile(pos, null);
            }
        }
    }

    private void Update()
    {
        // ���ŵ� Ÿ���� ��ġ�� ���� �ݺ��մϴ�.
        foreach (var pos in vector3Ints)
        {
            // �ش� ��ġ�� ��ü Ÿ�ϰ� ��� Ÿ���� ��� ���� ���
            if (!OBJTilemap.HasTile(pos) && !BGTilemap.HasTile(pos))
            {
                // ��� Ÿ�ϸʿ� RuleTile�� �����Ͽ� ���� ���·� �ǵ����ϴ�.
                BGTilemap.SetTile(pos, ruleTile);
            }
        }
    }
}
