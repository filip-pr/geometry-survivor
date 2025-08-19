using UnityEngine;

[System.Serializable]
public class PlayerItemData : IWeightedItem
{
    [field: SerializeField] public GameObject ItemPrefab { get; private set; }
    [field: SerializeField] public float DropWeight { get; set; }
    public GameObject ItemInstance { get; set; }
    public float Weight => DropWeight;
}
    