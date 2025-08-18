using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private float experience = 0f;

    private float ExperienceNeeded => level * 100f;

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
    }
}
