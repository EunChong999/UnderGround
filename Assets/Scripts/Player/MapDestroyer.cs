using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
using Unity.VisualScripting;

public class MapDestroyer : MonoBehaviour
{
    [SerializeField] private Tilemap gamePlayTilemap; // ���� �÷��̿� ���Ǵ� Ÿ�ϸ�
    [SerializeField] private Tilemap objectTilemap; // ������Ʈ�� ��ġ�ϴ� Ÿ�ϸ�
    [SerializeField] private RuleTile wallTile; // �� Ÿ��
    [SerializeField] private Tile destructibleTile; // �ı� ������ Ÿ��
    [SerializeField] private GameObject explosionPrefeb; // ���� ȿ�� ������
    [SerializeField] private int explosionRange; // ���� ����
    [SerializeField] private float interval; // ���� ����

    public Bounds updateBounds; // ������Ʈ �� Bounds ����

    // ���� ȿ���� ���õ� ��ü���� Ǯ
    public Queue<GameObject> pooledUpObjects;
    public Queue<GameObject> pooledLeftObjects;
    public Queue<GameObject> pooledDownObjects;
    public Queue<GameObject> pooledRightObjects;

    // ���� ȿ���� ���õ� Ÿ�ϵ��� ��ġ Ǯ
    public Queue<Vector3Int> pooledUpTilesPos;
    public Queue<Vector3Int> pooledLeftTilesPos;
    public Queue<Vector3Int> pooledDownTilesPos;
    public Queue<Vector3Int> pooledRightTilesPos;

    // Bounds�� �ð�ȭ�ϴ� �Լ�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // �ð�ȭ ���� ����
        Gizmos.DrawWireCube(updateBounds.center, updateBounds.size); // Bounds�� �ð�ȭ
    }

    private void Start()
    {
        AstarPath.active.UpdateGraphs(updateBounds, 0); // A* Pathfinding �׷��� ������Ʈ
    }

    // ���� �Լ�
    public void Explode(Transform worldPos)
    {
        // �ʱ�ȭ
        pooledUpObjects = new Queue<GameObject>();
        pooledLeftObjects = new Queue<GameObject>();
        pooledDownObjects = new Queue<GameObject>();
        pooledRightObjects = new Queue<GameObject>();
        pooledUpTilesPos = new Queue<Vector3Int>();
        pooledLeftTilesPos = new Queue<Vector3Int>();
        pooledDownTilesPos = new Queue<Vector3Int>();
        pooledRightTilesPos = new Queue<Vector3Int>();

        // ���� �߻� ��ġ�� �� ���
        Vector3Int originCell = gamePlayTilemap.WorldToCell(worldPos.position);

        // �߾� ���� ȿ�� ����
        ExplodeCellCheck(worldPos, originCell, "middle");

        // �����¿� ���� ȿ�� �� Ÿ�� �ı� ����
        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i + 1, 0), "up")) { }
            else
            {
                break;
            }
        }

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int((i * (-1)) - 1, 0, 0), "left")) { }
            else
            {
                break;
            }
        }

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, (i * (-1)) - 1, 0), "down")) { }
            else
            {
                break;
            }
        }

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(i + 1, 0, 0), "right")) { }
            else
            {
                break;
            }
        }

        // ���� ȿ�� �� Ÿ�� �ı� �ڷ�ƾ ����
        StartCoroutine(ExplodeCell(pooledUpObjects));
        StartCoroutine(ExplodeTile(pooledUpTilesPos));

        StartCoroutine(ExplodeCell(pooledLeftObjects));
        StartCoroutine(ExplodeTile(pooledLeftTilesPos));

        StartCoroutine(ExplodeCell(pooledDownObjects));
        StartCoroutine(ExplodeTile(pooledDownTilesPos));

        StartCoroutine(ExplodeCell(pooledRightObjects));
        StartCoroutine(ExplodeTile(pooledRightTilesPos));
    }

    // ���� ȿ�� ��ü���� Ȱ��ȭ
    IEnumerator ExplodeCell(Queue<GameObject> pooledObjects)
    {
        while (pooledObjects.Count > 0)
        {
            yield return new WaitForSeconds(interval);
            pooledObjects.Dequeue().SetActive(true);
        }
        yield break;
    }

    // ���߷� ���� �ı��� Ÿ�ϵ� ó��
    IEnumerator ExplodeTile(Queue<Vector3Int> pooledTilesPos)
    {
        while (pooledTilesPos.Count > 0)
        {
            yield return new WaitForSeconds(interval);
            objectTilemap.SetTile(pooledTilesPos.Dequeue(), null); // �ı��� Ÿ�� ����
            AstarPath.active.UpdateGraphs(updateBounds, 0); // A* Pathfinding �׷��� ������Ʈ
        }
    }

    // ���� ȿ�� �� Ÿ�� �ı� üũ �Լ�
    bool ExplodeCellCheck(Transform worldPos, Vector3Int originCell, string str)
    {
        RuleTile gamePlayRuleTile = gamePlayTilemap.GetTile<RuleTile>(originCell);

        if (gamePlayRuleTile == wallTile)
        {
            return false;
        }

        Vector3 pos = gamePlayTilemap.GetCellCenterWorld(originCell);

        // ���� ȿ�� �� �ı��� Ÿ�� ����
        switch (str)
        {
            case "middle":
                Instantiate(explosionPrefeb, pos, worldPos.rotation);
                break;
            case "up":
                GameObject objUp = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objUp.SetActive(false);
                pooledUpObjects.Enqueue(objUp);
                pooledUpTilesPos.Enqueue(originCell);
                break;
            case "left":
                GameObject objLeft = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objLeft.SetActive(false);
                pooledLeftObjects.Enqueue(objLeft);
                pooledLeftTilesPos.Enqueue(originCell);
                break;
            case "down":
                GameObject objDown = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objDown.SetActive(false);
                pooledDownObjects.Enqueue(objDown);
                pooledDownTilesPos.Enqueue(originCell);
                break;
            case "right":
                GameObject objRight = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objRight.SetActive(false);
                pooledRightObjects.Enqueue(objRight);
                pooledRightTilesPos.Enqueue(originCell);
                break;
            default:
                break;
        }

        return true;
    }
}
