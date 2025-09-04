using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to handle individual upgrade logic and UI.
/// </summary>
public class UpgradeHandler : MonoBehaviour
{
    [field: SerializeField] public string UpgradeName { get; private set; }
    [SerializeField] private int baseUpgradeCost = 1000;

    [SerializeField] private float upgradeIncreaseStep = 10f;

    [SerializeField] TextMeshProUGUI upgradeAmountText;
    [SerializeField] TextMeshProUGUI upgradeCostText;
    [SerializeField] Button upgradeIncreaseButton;
    [SerializeField] Button upgradeDecreaseButton;

    [SerializeField] UpgradeManager upgradeManager;

    private int maxUpgradeLevel = 10;
    private int upgradeLevel;

    public float UpgradeAmount => upgradeIncreaseStep * upgradeLevel;
    private int NextUpgradeCost => baseUpgradeCost * (upgradeLevel + upgradeIncrease + 1);
    private int CurrentUpgradeCost => baseUpgradeCost * (upgradeLevel + upgradeIncrease);

    private int upgradeIncrease = 0;

    /// <summary>
    /// Update the UI elements to reflect the current upgrade state.
    /// </summary>
    public void UpdateUI()
    {
        if (upgradeLevel + upgradeIncrease >= maxUpgradeLevel)
        {
            upgradeCostText.text = "MAX";
        }
        else
        {
            upgradeCostText.text = $"{NextUpgradeCost.ToString()} UP";
        }
        if (upgradeIncrease > 0) { 
            upgradeAmountText.text = $"+{UpgradeAmount.ToString()} (+{(upgradeIncrease * upgradeIncreaseStep).ToString()}) %";
        }
        else{
            upgradeAmountText.text = $"+{UpgradeAmount.ToString()} %";
        }
        upgradeDecreaseButton.interactable = upgradeIncrease > 0;
        upgradeIncreaseButton.interactable = (upgradeLevel + upgradeIncrease) < maxUpgradeLevel && upgradeManager.FreeUpgradePoints >= NextUpgradeCost;
    }

    private void Awake()
    {
        upgradeLevel = PlayerPrefs.GetInt(UpgradeName, 0);
    }

    /// <summary>
    /// Confirm the upgrade increases, applying them to the upgrade level and saving to PlayerPrefs.
    /// </summary>
    public void ConfirmUpgradeIncrease()
    {
        upgradeLevel += upgradeIncrease;
        upgradeIncrease = 0;
        PlayerPrefs.SetInt(UpgradeName, upgradeLevel);
    }

    /// <summary>
    /// Cancel the upgrade increases, reverting any changes.
    /// </summary>
    public void CancelUpgradeIncrease()
    {
        while (upgradeIncrease > 0)
        {
            DecreaseUpgradeIncrease();
        }
    }

    /// <summary>
    /// Increase the upgrade increase, updating the points held and UI accordingly.
    /// </summary>
    public void IncreaseUpgradeIncrease()
    {
        upgradeManager.AddUpgradePointsToHold(NextUpgradeCost);
        upgradeIncrease++;
        upgradeManager.UpdateUI();
    }

    /// <summary>
    /// Decrease the upgrade increase, updating the points held and UI accordingly.
    /// </summary>
    public void DecreaseUpgradeIncrease()
    {
        upgradeManager.AddUpgradePointsToHold(-CurrentUpgradeCost);
        upgradeIncrease--;
        upgradeManager.UpdateUI();
    }
}
