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
    [SerializeField] private float interval;

    [SerializeField] private AstarPath astar;

    public Bounds updateBounds;

    public List<GameObject> pooledUpObjects;
    public List<GameObject> pooledLeftObjects;
    public List<GameObject> pooledDownObjects;
    public List<GameObject> pooledRightObjects;

    [Space(25)]

    public List<Vector3Int> pooledUpTilesPos;
    public List<Vector3Int> pooledLeftTilesPos;
    public List<Vector3Int> pooledDownTilesPos;
    public List<Vector3Int> pooledRightTilesPos;

    // Bounds를 시각화하는 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 시각화 색상 설정
        Gizmos.DrawWireCube(updateBounds.center, updateBounds.size); // Bounds를 시각화
    }

    public void Explode(Transform worldPos)
    {
        pooledUpObjects = new List<GameObject>();
        pooledLeftObjects = new List<GameObject>();
        pooledDownObjects = new List<GameObject>();
        pooledRightObjects = new List<GameObject>();

        pooledUpTilesPos = new List<Vector3Int>();
        pooledLeftTilesPos = new List<Vector3Int>();
        pooledDownTilesPos = new List<Vector3Int>();
        pooledRightTilesPos = new List<Vector3Int>();

        Vector3Int originCell = gamePlayTilemap.WorldToCell(worldPos.position);

        ExplodeCellCheck(worldPos, originCell, "middle");

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i + 1, 0), "up")) { }
            else
            {
                StartCoroutine(ExplodeCell(pooledUpObjects));
                StartCoroutine(ExplodeTile(pooledUpTilesPos));
                break;
            }
        }

        for (int i = 0; i < explosionRange; i--)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(i - 1, 0, 0), "left")) { }
            else
            {
                StartCoroutine(ExplodeCell(pooledLeftObjects));
                StartCoroutine(ExplodeTile(pooledLeftTilesPos));
                break;
            }
        }

        for (int i = 0; i < explosionRange; i--)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(0, i - 1, 0), "down")) { }
            else
            {
                StartCoroutine(ExplodeCell(pooledDownObjects));
                StartCoroutine(ExplodeTile(pooledDownTilesPos));
                break;
            }
        }

        for (int i = 0; i < explosionRange; i++)
        {
            if (ExplodeCellCheck(worldPos, originCell + new Vector3Int(i + 1, 0, 0), "right")) { }
            else
            {
                StartCoroutine(ExplodeCell(pooledRightObjects));
                StartCoroutine(ExplodeTile(pooledRightTilesPos));
                break;
            }
        }
    }

    // 코루틴 함수 정의
    IEnumerator ExplodeCell(List<GameObject> pooledObjects)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            yield return new WaitForSeconds(interval);

            pooledObjects[i].SetActive(true);
        }

        yield break;
    }

    IEnumerator ExplodeTile(List<Vector3Int> pooledTilesPos)
    {
        for (int i = 0; i < pooledTilesPos.Count; i++)
        {
            yield return new WaitForSeconds(interval);

            objectTilemap.SetTile(pooledTilesPos[i], null);

            AstarPath.active.UpdateGraphs(updateBounds, 0);
        }

        yield break;
    }

    bool ExplodeCellCheck(Transform worldPos, Vector3Int originCell, string str)
    {
        RuleTile gamePlayRuleTile = gamePlayTilemap.GetTile<RuleTile>(originCell);

        if (gamePlayRuleTile == wallTile)
        {
            return false;
        }

        Vector3 pos = gamePlayTilemap.GetCellCenterWorld(originCell);

        switch (str)
        {
            case "middle":
                Instantiate(explosionPrefeb, pos, worldPos.rotation);
                break;
            case "up":
                GameObject objUp = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objUp.SetActive(false);
                pooledUpObjects.Add(objUp);

                Tile objectUpTile = objectTilemap.GetTile<Tile>(originCell);

                if (objectUpTile == destructibleTile)
                {
                    pooledUpTilesPos.Add(originCell);
                }

                break;
            case "left":
                GameObject objLeft = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objLeft.SetActive(false);
                pooledLeftObjects.Add(objLeft);

                Tile objectLeftTile = objectTilemap.GetTile<Tile>(originCell);

                if (objectLeftTile == destructibleTile)
                {
                    pooledLeftTilesPos.Add(originCell);
                }

                break;
            case "down":
                GameObject objDown = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objDown.SetActive(false);
                pooledDownObjects.Add(objDown);

                Tile objectDownTile = objectTilemap.GetTile<Tile>(originCell);

                if (objectDownTile == destructibleTile)
                {
                    pooledDownTilesPos.Add(originCell);
                }

                break;
            case "right":
                GameObject objRight = Instantiate(explosionPrefeb, pos, worldPos.rotation);
                objRight.SetActive(false);
                pooledRightObjects.Add(objRight);

                Tile objectRightTile = objectTilemap.GetTile<Tile>(originCell);

                if (objectRightTile == destructibleTile)
                {
                    pooledRightTilesPos.Add(originCell);
                }

                break;
            default:
                break;
        }

        return true;
    }
}
