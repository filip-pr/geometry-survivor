using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to manage player leveling, experience gain, and level-up behavior.
/// </summary>
[RequireComponent(typeof(PlayerStats))]
public class PlayerLevel : MonoBehaviour
{
    [field: SerializeField] public int Level { get; private set; } = 1;
    [SerializeField] private float experience = 0f;
    [SerializeField] private float experienceRequiredMultiplier = 250f;
    public StatModifier ExperienceGainModifier { get; }

    [SerializeField] private GameObject levelTextPrefab;
    [SerializeField] private GameObject experienceBarPrefab;
    [SerializeField] private GameObject levelUpPopoupPrefab;

    private TextMeshProUGUI levelText;
    private Slider experienceBar;
    private GameObject levelUpPopoup;

    private float ExperienceNeeded => Level * experienceRequiredMultiplier;

    private string LevelText => $"Lvl: {Level}";

    /// <summary>
    /// Setup the level HUD elements on the provided canvas.
    /// </summary>
    public void SetupLevelHUD(Canvas canvas)
    {
        levelText = Instantiate(levelTextPrefab, canvas.transform).GetComponent<TextMeshProUGUI>();        
        levelText.text = LevelText;

        experienceBar = Instantiate(experienceBarPrefab, canvas.transform).GetComponent<Slider>();
        experienceBar.maxValue = ExperienceNeeded;

        levelUpPopoup = Instantiate(levelUpPopoupPrefab, canvas.transform);
        levelUpPopoup.SetActive(false);
    }

    private void OnDestroy()
    {
        if (levelText != null)
        {
            Destroy(levelText.gameObject);
        }
        if (experienceBar != null)
        {
            Destroy(experienceBar.gameObject);
        }
        if (levelUpPopoup != null)
        {
            Destroy(levelUpPopoup);
        }
    }

    /// <summary>
    /// Handle actions to perform when the player levels up.
    /// </summary>
    private void OnLevelUp()
    {
        string levelUpInfo = "";
        GetComponent<Health>().Heal(float.PositiveInfinity);
        levelUpInfo += "Health Restored!\n";
        PlayerItem levelUpItem = GetComponent<PlayerInventory>().AddOrUpgradeItem();
        if (levelUpItem != null)
        {
            if (levelUpItem.Level == 1)
            {
                levelUpInfo += $"New Item: {levelUpItem.ItemName}!\n";
            }
            else
            {
                levelUpInfo += $"{levelUpItem.ItemName} upgraded!\n";
            }
        }
        else
        {
            levelUpInfo += $"All items at max level\n";
        }
        levelText.text = LevelText;
        experienceBar.maxValue = ExperienceNeeded;
        levelUpPopoup.transform.Find("LevelUpInfo").GetComponent<TextMeshProUGUI>().text = levelUpInfo;
        levelUpPopoup.SetActive(true);
    }

    /// <summary>
    /// Add experience to the player and handle level-ups if experience threshold is crossed.
    /// </summary>
    public void GainExperience(float amount)
    {
        experience += ExperienceGainModifier == null ? amount : ExperienceGainModifier.Modify(amount);
        while (experience >= ExperienceNeeded)
        {
            experience -= ExperienceNeeded;
            Level++;
            OnLevelUp();
        }
        experienceBar.value = experience;
    }
}
