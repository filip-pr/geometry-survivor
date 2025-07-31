
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private int renderDistance = 40;

    [SerializeField] private GameObject groundPrefab;

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

    void TryGenerateTile(Vector2Int position)
    {
        if (!groundTiles.ContainsKey(position))
        {
            GameObject newTile = Instantiate(groundPrefab, position * tileSize, Quaternion.identity, transform);
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
