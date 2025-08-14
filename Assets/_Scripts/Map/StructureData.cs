using UnityEngine;

[System.Serializable]
public struct StructureData : IWeightedItem
{
    public GameObject structurePrefab;
    public float spawnWeight;

    public readonly float Weight => spawnWeight;
}
