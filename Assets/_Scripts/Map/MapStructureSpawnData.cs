using UnityEngine;

/// <summary>
/// Helper struct to hold data for spawning different map structures.
/// </summary
[System.Serializable]
public struct MapStructureSpawnData : IWeightedItem
{
    [field: SerializeField] public GameObject StructurePrefab { get; private set; }
    [field: SerializeField] public float SpawnWeight { get; private set; }

    public readonly float Weight => SpawnWeight;
}
