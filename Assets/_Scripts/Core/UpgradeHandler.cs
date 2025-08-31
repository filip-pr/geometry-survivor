using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void UpdateUI()
    {
        upgradeCostText.text = $"{NextUpgradeCost.ToString()} UP";
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

    public void ConfirmUpgradeIncrease()
    {
        upgradeLevel += upgradeIncrease;
        upgradeIncrease = 0;
        PlayerPrefs.SetInt(UpgradeName, upgradeLevel);
    }

    public void CancelUpgradeIncrease()
    {
        while (upgradeIncrease > 0)
        {
            DecreaseUpgradeIncrease();
        }
    }

    public void IncreaseUpgradeIncrease()
    {
        upgradeManager.UpdateUpgradePointsToHold(NextUpgradeCost);
        upgradeIncrease++;
        upgradeManager.UpdateUI();
    }

    public void DecreaseUpgradeIncrease()
    {
        upgradeManager.UpdateUpgradePointsToHold(-CurrentUpgradeCost);
        upgradeIncrease--;
        upgradeManager.UpdateUI();
    }
}
