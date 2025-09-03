using UnityEngine;

/// <summary>
/// Helper struct to hold data for spawning different enemy types.
/// </summary>
[System.Serializable]
public struct EnemySpawnData : IWeightedItem
{
    [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    [field: SerializeField] public float SpawnWeight { get; private set; }
    [field: SerializeField] public int SpawnPointCost { get; private set; }

    public float Weight => SpawnWeight;
}
