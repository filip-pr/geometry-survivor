using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private float experience = 0f;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider experienceBar;

    private float ExperienceNeeded => level * 100f;

    private string LevelText => $"Level: {level}";

    private void Start()
    {
        levelText.text = LevelText;
        experienceBar.maxValue = ExperienceNeeded;
    }

    private void OnLevelUp()
    {
        if (TryGetComponent<Health>(out var health))
        {
            health.Heal(health.MaxHeath);
        }
        if (TryGetComponent <PlayerInventory>(out var inventory))
        {
            inventory.AddOrUpgradeItem();
        }
        levelText.text = LevelText;
        experienceBar.maxValue = ExperienceNeeded;
    }

    public void GainExperience(float amount)
    {
        experience += amount;
        while (experience >= ExperienceNeeded)
        {
            experience -= ExperienceNeeded;
            level++;
            OnLevelUp();
        }
        experienceBar.value = experience;
    }
}
