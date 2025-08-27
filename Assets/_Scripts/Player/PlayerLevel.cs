using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private float experience = 0f;
    public StatModifier ExperienceGainModifier { get; }

    [SerializeField] private GameObject levelTextPrefab;
    [SerializeField] private GameObject experienceBarPrefab;
    [SerializeField] private GameObject levelUpPopoupPrefab;

    private TextMeshProUGUI levelText;
    private Slider experienceBar;
    private GameObject levelUpPopoup;

    private float ExperienceNeeded => level * 100f;

    private string LevelText => $"Lvl: {level}";

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


    public void GainExperience(float amount)
    {
        experience += ExperienceGainModifier == null ? amount : ExperienceGainModifier.Modify(amount);
        while (experience >= ExperienceNeeded)
        {
            experience -= ExperienceNeeded;
            level++;
            OnLevelUp();
        }
        experienceBar.value = experience;
    }
}
