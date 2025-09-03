using UnityEngine;

/// <summary>
/// Class to hold data for player items.
/// </summary>
[System.Serializable]
public class PlayerItemData : IWeightedItem
{
    [field: SerializeField] public GameObject ItemPrefab { get; private set; }
    [field: SerializeField] public float DropWeight { get; set; }
    public GameObject ItemInstance { get; set; }
    public GameObject ItemSlot { get; set; }
    public float Weight => DropWeight;
}
    