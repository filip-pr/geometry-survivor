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

    private TextMeshProUGUI levelText;
    private Slider experienceBar;

    private float ExperienceNeeded => level * 100f;

    private string LevelText => $"Lvl: {level}";

    public void SetupLevelHUD(Canvas canvas)
    {
        levelText = Instantiate(levelTextPrefab, transform).GetComponent<TextMeshProUGUI>();
        levelText.transform.SetParent(canvas.transform, false);
        levelText.text = LevelText;

        experienceBar = Instantiate(experienceBarPrefab, transform).GetComponent<Slider>();
        experienceBar.transform.SetParent(canvas.transform, false);
        experienceBar.maxValue = ExperienceNeeded;
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
    }

    private void OnLevelUp()
    {
        GetComponent<Health>().Heal(float.PositiveInfinity);
        PlayerItemData levelUpItem = GetComponent<PlayerInventory>().AddOrUpgradeItem();
        
        levelText.text = LevelText;
        experienceBar.maxValue = ExperienceNeeded;
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
