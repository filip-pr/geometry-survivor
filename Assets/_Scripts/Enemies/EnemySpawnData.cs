using UnityEngine;

[System.Serializable]
public class EnemySpawnData : IWeightedItem
{
    [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    [field: SerializeField] public float SpawnWeight { get; private set; }
    [field: SerializeField] public int SpawnPointCost { get; private set; }

    public float Weight => SpawnWeight;
}
