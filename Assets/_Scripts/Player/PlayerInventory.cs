using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    private const int maxItems = 5;

    [SerializeField] private PlayerItemData[] allItemsData;

    [SerializeField] private List<GameObject> heldItems = new();

    private bool IsFull => heldItems.Count >= maxItems;

    private void Start()
    {
        AddItem(allItemsData[0]);
    }

    private void AddItem(PlayerItemData item)
    {
        GameObject itemInstance = Instantiate(item.ItemPrefab, transform);
        heldItems.Add(itemInstance);
        item.ItemInstance = itemInstance;
    }

    public void AddOrUpgradeItem() // Should be prompting the player for choice of 3
    {
        PlayerItemData choice = WeightedRandom.Choose(allItemsData);

        if (choice.ItemInstance == null)
        {
            AddItem(choice);
            if (IsFull)
            {
                foreach (PlayerItemData item in allItemsData)
                {
                    if (item.ItemInstance == null)
                    {
                        item.DropWeight = 0;
                    }
                }
            }
        }
        else
        {
            PlayerItem item = choice.ItemInstance.GetComponent<PlayerItem>();
            item.LevelUp();
            if (item.Level >= item.MaxLevel)
            {
                choice.DropWeight = 0f;
            }
        }
    }
}
