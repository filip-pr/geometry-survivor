using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to handle overall upgrade management and UI.
/// </summary>
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
    }

    private void Start()
    {
        UpdateUI();
    }

    /// <summary>
    /// Update the all upgrade UI elements to reflect the current upgrade points and states.
    /// </summary>
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

    /// <summary>
    /// Confirm the upgrade increases of all upgrade handlers, deducting the held upgrade points.
    /// </summary>
    public void ConfirmUpgradesIncrease()
    {
        foreach (var upgradeHandler in upgradeHandlers)
        {
            upgradeHandler.ConfirmUpgradeIncrease();
        }
        upgradePoints -= upgradePointsHold;
        upgradePointsHold = 0;
        PlayerPrefs.SetInt(UpgradePointsKey, upgradePoints);
        PlayerPrefs.Save();
        UpdateUI();
    }

    /// <summary>
    /// Cancel the upgrade increases of all upgrade handlers, reverting any changes and resetting held upgrade points.
    /// </summary>
    public void CancelUpgradesIncrease()
    {
        foreach (var upgradeHandler in upgradeHandlers)
        {
            upgradeHandler.CancelUpgradeIncrease();
        }
        upgradePointsHold = 0;
        UpdateUI();
    }

    /// <summary>
    /// Add upgrade points to the upgrade points hold.
    /// </summary>
    public void AddUpgradePointsToHold(int amount)
    {
        upgradePointsHold += amount;
        UpdateUI();
    }

    /// <summary>
    /// Add upgrade points to the upgrade points total.
    /// </summary>
    public void AddUpgradePoints(int amount)
    {
        upgradePoints += amount;
        UpdateUI();
        PlayerPrefs.SetInt(UpgradePointsKey, upgradePoints);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Get the UpgradeHandler by its upgrade name.
    /// </summary>
    public UpgradeHandler GetUpgradeHandler(string upgradeName)
    {
        foreach (var upgradeHandler in upgradeHandlers)
        {
            if (upgradeHandler.UpgradeName == upgradeName)
            {
                return upgradeHandler;
            }
        }
        return null;
    }
}
