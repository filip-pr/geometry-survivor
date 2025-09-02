using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PlayerStats))]
public class PlayerInventory : MonoBehaviour
{
    private const int maxItems = 5;
    public Transform ProjectileParent { get; set; }

    [SerializeField] private PlayerItemData[] allItemsData;

    [SerializeField] private int itemsHeld = 0;

    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private float itemSlotSpacing = 10f;
    [SerializeField] private float itemSlotYPosition = -400f;

    private List<GameObject> itemSlots = new();
    private PlayerStats playerStats;

    private bool IsFull => itemsHeld >= maxItems;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        AddItem(allItemsData[0]);
    }

    public void SetupItemSlots(Canvas canvas)
    {
        float itemSlotSize = itemSlotPrefab.GetComponent<RectTransform>().rect.width;
        float totalWidth = itemSlotSize * maxItems;
        float startX = -totalWidth / 2 + itemSlotSize / 2;
        for (int i = 0; i < maxItems; i++)
        {
            GameObject itemSlotInstance = Instantiate(itemSlotPrefab, canvas.transform);
            itemSlotInstance.transform.localPosition = new Vector3(startX, itemSlotYPosition, 0);
            itemSlots.Add(itemSlotInstance);
            startX += itemSlotSize + itemSlotSpacing;
        }
    }

    private void OnDestroy()
    {
        foreach (var itemSlotInstance in itemSlots)
        {
            Destroy(itemSlotInstance);
        }
    }

    private string GetItemLevelText(int level)
    {
        return $"Lvl: {level.ToString()}";
    }

    private void AddItem(PlayerItemData item)
    {
        GameObject itemInstance = Instantiate(item.ItemPrefab, transform);
        itemInstance.GetComponent<PlayerItem>().ProjectileParent = ProjectileParent;
        itemInstance.GetComponent<PlayerItem>().SetupModifiers(playerStats);

        item.ItemInstance = itemInstance;
        item.ItemSlot =  itemSlots[itemsHeld];

        Transform itemSlotName = item.ItemSlot.transform.Find("ItemName");
        itemSlotName.GetComponent<TextMeshProUGUI>().text = itemInstance.GetComponent<PlayerItem>().ItemName;
        itemSlotName.GetComponent<TextMeshProUGUI>().enabled = true;

        Transform itemSlotLevel = item.ItemSlot.transform.Find("ItemLevel");
        itemSlotLevel.GetComponent<TextMeshProUGUI>().text = GetItemLevelText(itemInstance.GetComponent<PlayerItem>().Level);
        itemSlotLevel.GetComponent<TextMeshProUGUI>().enabled = true;

        itemsHeld++;
    }

    public PlayerItem AddOrUpgradeItem()
    {
        PlayerItemData randomChoice = WeightedRandom.Choose(allItemsData);
        if( randomChoice == null) return null;
        if (randomChoice.ItemInstance == null)
        {
            AddItem(randomChoice);
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
            PlayerItem item = randomChoice.ItemInstance.GetComponent<PlayerItem>();
            item.LevelUp();

            Transform itemSlotLevel = randomChoice.ItemSlot.transform.Find("ItemLevel");
            itemSlotLevel.GetComponent<TextMeshProUGUI>().text = GetItemLevelText(randomChoice.ItemInstance.GetComponent<PlayerItem>().Level);

            if (item.Level >= item.MaxLevel)
            {
                randomChoice.DropWeight = 0f;
            }
        }
        return randomChoice.ItemInstance.GetComponent<PlayerItem>();
    }
}
