
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Transform generationCenter;
    [SerializeField] private int generationDistance = 10;

    [SerializeField] private GameObject mapTilePrefab;
    [SerializeField] private MapStructureSpawnData[] structures;
    [SerializeField] private int structureSpawnTries = 20;
    [SerializeField] private float structureSpawnChance = 0.5f;

    private Vector2 mapTileSize;
    private Dictionary<Vector2Int, GameObject> mapTiles;
    private Vector2Int generationCenterTile;

    Vector2Int GetMapTilePosition(Vector2 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt((position.x + mapTileSize.x / 2) / mapTileSize.x),
            Mathf.FloorToInt((position.y + mapTileSize.y / 2) / mapTileSize.y)
        );
    }

    void Start()
    {
        mapTileSize = mapTilePrefab.GetComponent<SpriteRenderer>().size;
        mapTiles = new();
        generationCenterTile = GetMapTilePosition(generationCenter.position);
        GenerateMapTiles();
    }

    void AddStructures(GameObject tile)
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

    void GenerateMapTile(Vector2Int position)
    {
        if (!mapTiles.ContainsKey(position))
        {
            GameObject newTile = Instantiate(mapTilePrefab, position * mapTileSize, Quaternion.identity, transform);
            AddStructures(newTile);
            mapTiles[position] = newTile;
        }
    }

    void GenerateMapTiles()
    {
        int countX = Mathf.CeilToInt(generationDistance / mapTileSize.x);
        int countY = Mathf.CeilToInt(generationDistance / mapTileSize.y);
        for (int x = -countX; x <= countX; x++)
        {
            for (int y = -countY; y <= countY; y++)
            {
                Vector2Int tilePosition = generationCenterTile + new Vector2Int(x, y);
                GenerateMapTile(tilePosition);
            }
        }
    }

    void Update()
    {
        Vector2Int newCenterTile = GetMapTilePosition(generationCenter.position);
        if (newCenterTile != generationCenterTile)
        {
            generationCenterTile = newCenterTile;
            GenerateMapTiles();
        }
    }
}
