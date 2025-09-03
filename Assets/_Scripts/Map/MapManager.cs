
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to manage dynamic map tile generation and destruction based on target position.
/// </summary>
public class MapManager : MonoBehaviour
{
    [field: SerializeField] public Transform GenerationCenter { get; set; }
    [field: SerializeField] public float GenerationDistance { get; set; } = 30;
    [field: SerializeField] public float DestroyDistance { get; set; } = 50;

    [SerializeField] private GameObject mapTilePrefab;
    [SerializeField] private MapStructureSpawnData[] structures;
    [SerializeField] private int structureSpawnTries = 20;
    [SerializeField] private float structureSpawnChance = 0.5f;

    [SerializeField] private int seedOffset;

    private Vector2 mapTileSize;
    private Dictionary<Vector2Int, GameObject> mapTiles;
    private Vector2Int generationCenterTile;

    /// <summary>
    /// Get the map tile position for a given world position.
    /// </summary>
    private Vector2Int GetMapTilePosition(Vector2 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt((position.x + mapTileSize.x / 2) / mapTileSize.x),
            Mathf.FloorToInt((position.y + mapTileSize.y / 2) / mapTileSize.y)
        );
    }

    private void Start()
    {
        seedOffset = Random.Range(0, int.MaxValue);
        mapTileSize = mapTilePrefab.GetComponent<SpriteRenderer>().size;
        mapTiles = new();
        generationCenterTile = GetMapTilePosition(GenerationCenter.position);
        GenerateMapTiles();
    }

    /// <summary>
    /// Randomly add structures to the given map tile.
    /// </summary>
    private void AddStructures(GameObject tile)
    {
        List<GameObject> placedStructures = new();
        for (int i = 0; i < structureSpawnTries; i++)
        {
            if (Random.value > structureSpawnChance) continue;
            bool structureFits = true;
            GameObject randomStructure = WeightedRandom.Choose(structures).StructurePrefab;
            Vector2 structureSize = randomStructure.GetComponent<SpriteRenderer>().bounds.size;
            Vector2 structurePosition = tile.transform.position + new Vector3(
                Random.Range(-mapTileSize.x / 2 + structureSize.x / 2, mapTileSize.x / 2 - structureSize.x / 2),
                Random.Range(-mapTileSize.y / 2 + structureSize.y / 2, mapTileSize.y / 2 - structureSize.y / 2)
            );
            foreach (var placedStructure in placedStructures)
            {
                if (placedStructure.GetComponent<SpriteRenderer>().bounds.Intersects(
                    new Bounds(structurePosition, structureSize)))
                {
                    structureFits = false;
                    break;
                }
            }
            if (!structureFits) continue;
            placedStructures.Add(Instantiate(randomStructure, structurePosition, Quaternion.identity, tile.transform));
        }
    }

    /// <summary>
    /// Generate a map tile at the given tile position if it doesn't already exist.
    /// </summary>
    private void TryGenerateMapTile(Vector2Int position)
    {
        if (!mapTiles.ContainsKey(position))
        {
            GameObject newTile = Instantiate(mapTilePrefab, position * mapTileSize, Quaternion.identity, transform);
            Random.InitState((position.x * 1000) + position.y + seedOffset);
            AddStructures(newTile);
            mapTiles[position] = newTile;
        }
    }

    /// <summary>
    /// Generate map tiles around the generation center within the generation distance.
    /// </summary>
    private void GenerateMapTiles()
    {
        int countX = Mathf.CeilToInt(GenerationDistance / mapTileSize.x);
        int countY = Mathf.CeilToInt(GenerationDistance / mapTileSize.y);
        for (int x = -countX; x <= countX; x++)
        {
            for (int y = -countY; y <= countY; y++)
            {
                Vector2Int tilePosition = generationCenterTile + new Vector2Int(x, y);
                TryGenerateMapTile(tilePosition);
            }
        }
    }

    /// <summary>
    /// Destroy map tiles that are beyond the destroy distance from the generation center.
    /// </summary>
    private void DestroyTiles()
    {
        List<Vector2Int> tilesToDestroy = new List<Vector2Int>();
        foreach (var tile in mapTiles)
        {
            Vector2 distance = (tile.Key * mapTileSize) - (Vector2)GenerationCenter.position;
            if (Mathf.Max(Mathf.Abs(distance.x), Mathf.Abs(distance.y)) > DestroyDistance)
            {
                tilesToDestroy.Add(tile.Key);
            }
        }
        foreach (var tilePosition in tilesToDestroy)
        {
            Destroy(mapTiles[tilePosition]);
            mapTiles.Remove(tilePosition);
        }
    }

    private void Update()
    {

        Vector2Int newCenterTile = GetMapTilePosition(GenerationCenter.position);
        if (newCenterTile != generationCenterTile)
        {
            generationCenterTile = newCenterTile;
            GenerateMapTiles();
            DestroyTiles();
        }
    }
}
