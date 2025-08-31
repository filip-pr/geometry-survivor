using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{

    [SerializeField] List<UpgradeHandler> upgradeHandlers;
    [SerializeField] TextMeshProUGUI upgradePointSummary;
    [SerializeField] Button confirmButton;
    [SerializeField] Button resetButton;

    private const string UpgradePointsKey = "UpgradePoints";

    [SerializeField] private int upgradePoints;
    private int upgradePointsHold = 0;
    public int FreeUpgradePoints => upgradePoints - upgradePointsHold;

    private void Awake()
    {
        upgradePoints = PlayerPrefs.GetInt(UpgradePointsKey, 0);
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (upgradePointsHold > 0)
        {
            upgradePointSummary.text = $"Upgrade Points: {upgradePoints} (-{upgradePointsHold})";
            confirmButton.interactable = true;
            resetButton.interactable = true;
        }
        else
        {
            upgradePointSummary.text = $"Upgrade Points: {upgradePoints}";
            confirmButton.interactable = false;
            resetButton.interactable = false;
        }
        foreach (var upgradeHandler in upgradeHandlers)
        {
            upgradeHandler.UpdateUI();
        }
    }

    public void ConfirmUpgradesIncrease()
    {
        foreach (var upgradeHandler in upgradeHandlers)
        {
            upgradeHandler.ConfirmUpgradeIncrease();
        }
        upgradePoints -= upgradePointsHold;
        upgradePointsHold = 0;
        PlayerPrefs.SetInt(UpgradePointsKey, upgradePoints);
        UpdateUI();
    }

    public void CancelUpgradesIncrease()
    {
        foreach (var upgradeHandler in upgradeHandlers)
        {
            upgradeHandler.CancelUpgradeIncrease();
        }
        upgradePointsHold = 0;
        UpdateUI();
    }

    public void UpdateUpgradePointsToHold(int amount)
    {
        upgradePointsHold += amount;
        UpdateUI();
    }

    public void AddUpgradePoints(int amount)
    {
        upgradePoints += amount;
        UpdateUI();
        PlayerPrefs.SetInt(UpgradePointsKey, upgradePoints);
    }

    private void OnValidate()
    {
        UpdateUI();
    }
}
