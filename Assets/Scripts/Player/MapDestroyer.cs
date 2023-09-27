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

    // Bounds�� �ð�ȭ�ϴ� �Լ�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // �ð�ȭ ���� ����
        Gizmos.DrawWireCube(updateBounds.center, updateBounds.size); // Bounds�� �ð�ȭ
    }

    private IEnumerator ExplodeUp(int explosionSize, Transform worldPos, Vector3Int cell) // ���� �Ÿ� ����ŭ ���� ������ �������� �Ͽ� ������ �������� ���������� �Ͷ߸���
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

            // �־��� bounds�� delay�� �׷��� ������Ʈ ����
            AstarPath.active.UpdateGraphs(updateBounds, 0);
        }

        Vector3 pos = gamePlayTilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefeb, pos, worldPos.rotation);
    }

    public void Explode(Transform worldPos)
    {
        // �÷��̾��� ���� ��ġ ����
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
                // �÷��̾��� ��ġ�� ��ź�� ��ġ ������ �Ÿ� �� 
                // �ش� ���� �ڷ�ƾ�� �Ű������� ���
                // ������ �ð� ���ݸ��� ȣ��� �Լ��� Coroutine���� �����մϴ�.
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

            // �־��� bounds�� delay�� �׷��� ������Ʈ ����
            AstarPath.active.UpdateGraphs(updateBounds, 0);
        }

        Vector3 pos = gamePlayTilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefeb, pos, worldPos.rotation);

        return true;
    }
}
