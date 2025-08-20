
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [field: SerializeField] public Transform GenerationCenter { get; set; }
    [field: SerializeField] public float GenerationDistance { get; set; } = 10;
    [field: SerializeField] public float DestroyDistance { get; set; } = float.PositiveInfinity;

    [SerializeField] private GameObject mapTilePrefab;
    [SerializeField] private MapStructureSpawnData[] structures;
    [SerializeField] private int structureSpawnTries = 20;
    [SerializeField] private float structureSpawnChance = 0.5f;

    private Vector2 mapTileSize;
    private Dictionary<Vector2Int, GameObject> mapTiles;
    private Vector2Int generationCenterTile;

    private Vector2Int GetMapTilePosition(Vector2 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt((position.x + mapTileSize.x / 2) / mapTileSize.x),
            Mathf.FloorToInt((position.y + mapTileSize.y / 2) / mapTileSize.y)
        );
    }

    private void Start()
    {
        mapTileSize = mapTilePrefab.GetComponent<SpriteRenderer>().size;
        mapTiles = new();
        generationCenterTile = GetMapTilePosition(GenerationCenter.position);
        GenerateMapTiles();
    }

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

    private void GenerateMapTile(Vector2Int position)
    {
        if (!mapTiles.ContainsKey(position))
        {
            GameObject newTile = Instantiate(mapTilePrefab, new Vector2(transform.position.x, transform.position.y) + position * mapTileSize, Quaternion.identity, transform);
            AddStructures(newTile);
            mapTiles[position] = newTile;
        }
    }

    private void GenerateMapTiles()
    {
        int countX = Mathf.CeilToInt(GenerationDistance / mapTileSize.x);
        int countY = Mathf.CeilToInt(GenerationDistance / mapTileSize.y);
        for (int x = -countX; x <= countX; x++)
        {
            for (int y = -countY; y <= countY; y++)
            {
                Vector2Int tilePosition = generationCenterTile + new Vector2Int(x, y);
                GenerateMapTile(tilePosition);
            }
        }
    }

    private void DestroyTiles()
    {
        List<Vector2Int> tilesToDestroy = new List<Vector2Int>();
        foreach (var tile in mapTiles)
        {
            Vector2 tileCoordinates = tile.Key * mapTileSize;
            if (Vector2.Distance(tileCoordinates, GenerationCenter.position) > DestroyDistance)
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

    public void Recenter()
    {
        DestroyTiles();
        Vector2Int tileOffset = generationCenterTile;
        Dictionary<Vector2Int, GameObject> newMapTiles = new();
        foreach (var tile in mapTiles)
        {
            Vector2Int newPosition = tile.Key - tileOffset;
            newMapTiles[newPosition] = tile.Value;
        }
        mapTiles = newMapTiles;

    }
    private void Update()
    {

        Vector2Int newCenterTile = GetMapTilePosition(GenerationCenter.position);
        if (newCenterTile != generationCenterTile)
        {
            generationCenterTile = newCenterTile;
            GenerateMapTiles();
            if (!float.IsPositiveInfinity(DestroyDistance))
            {
                DestroyTiles();
            }
        }
    }
}
