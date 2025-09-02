using UnityEngine;

public class HealthBoosterItem : PlayerItem
{
    public override string ItemName => "Health Booster";

    public override int MaxLevel => 5;

    private StatModifier healthModifier;
    private StatModifier healthRegenModifier;

    public override void SetupModifiers(PlayerStats playerStats)
    {
        healthModifier = playerStats.MaxHealthModifier;
        healthRegenModifier = playerStats.HealthRegenModifier;
    }

    protected override void OnLevelUp()
    {
        switch (Level)
        {
            case 1:
                healthModifier.IncreaseMultiplier(0.125f);
                healthRegenModifier.IncreaseMultiplier(0.25f);
                break;
            case 2:
                healthModifier.IncreaseMultiplier(0.25f);
                healthRegenModifier.IncreaseMultiplier(0.5f);
                break;
            case 3:
                healthModifier.IncreaseMultiplier(0.5f);
                healthRegenModifier.IncreaseMultiplier(1f);
                break;
            case 4:
                healthModifier.IncreaseMultiplier(1f);
                healthRegenModifier.IncreaseMultiplier(2f);
                break;
            case 5:
                healthModifier.IncreaseMultiplier(2f);
                healthRegenModifier.IncreaseMultiplier(4f);
                break;
        }
    }
}

