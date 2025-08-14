using UnityEngine;

[System.Serializable]
public struct EnemyData : IWeightedItem
{
    public GameObject enemyPrefab;
    public float spawnWeight;
    public int spawnPointCost;

    public readonly float Weight => spawnWeight;
}
