
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private int renderDistance = 40;

    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject[] objectsPrefabs;
    [SerializeField] private int objectSpawnTries = 20;
    [SerializeField] private float objectSpawnChance = 0.7f;

    private Vector2 tileSize;
    private Dictionary<Vector2Int, GameObject> groundTiles;
    private Vector2Int targetTile;

    void Start()
    {
        tileSize = groundPrefab.GetComponent<SpriteRenderer>().size;
        groundTiles = new();
        targetTile = GetTilePosition(target.position);
        GenerateTiles();
    }

    Vector2Int GetTilePosition(Vector2 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / tileSize.x),
            Mathf.FloorToInt(position.y / tileSize.y)
        );
    }

    void AddObjects(GameObject tile)
    {
        for (int i = 0; i < objectSpawnTries; i++)
        {
            if (Random.value > objectSpawnChance) continue;
            GameObject randomObject = objectsPrefabs[Random.Range(0, objectsPrefabs.Length)];
            Vector2 objectSize = randomObject.GetComponent<SpriteRenderer>().bounds.size;
            Vector2 objectPosition = tile.transform.position + new Vector3(
                Random.Range(-tileSize.x / 2 + objectSize.x / 2, tileSize.x / 2 - objectSize.x / 2),
                Random.Range(-tileSize.y / 2 + objectSize.y / 2, tileSize.y / 2 - objectSize.y / 2)
            );
            Instantiate(randomObject, objectPosition, Quaternion.identity, tile.transform);
        }
    }

    void TryGenerateTile(Vector2Int position)
    {
        if (!groundTiles.ContainsKey(position))
        {
            GameObject newTile = Instantiate(groundPrefab, position * tileSize, Quaternion.identity, transform);
            AddObjects(newTile);
            groundTiles[position] = newTile;
        }
    }

    void GenerateTiles()
    {
        int countX = Mathf.CeilToInt(renderDistance / tileSize.x);
        int countY = Mathf.CeilToInt(renderDistance / tileSize.y);

        for (int x = -countX; x <= countX; x++)
        {
            for (int y = -countY; y <= countY; y++)
            {
                Vector2Int tilePosition = targetTile + new Vector2Int(x, y);
                TryGenerateTile(tilePosition);
            }
        }
    }

    void Update()
    {
        Vector2Int newTargetTile = GetTilePosition(target.position);
        if (newTargetTile != targetTile)
        {
            targetTile = newTargetTile;
            GenerateTiles();
        }
    }
}
