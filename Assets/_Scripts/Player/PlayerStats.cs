
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public StatModifier MaxHealthModifier { get; set; }
    [field: SerializeField] public StatModifier HealthRegenModifier { get; set; }
    [field: SerializeField] public StatModifier MovementSpeedModifier { get; set; }
    [field: SerializeField] public StatModifier DamageModifier { get; set; }
    [field: SerializeField] public StatModifier KnockbackModifier { get; set; }
    [field: SerializeField] public StatModifier ExperienceGainModifier { get; set ; }
    [field: SerializeField] public StatModifier ExperienceMagnetRangeModifier { get; set; }

    public void SetUpgradeModifiers(UpgradeManager upgradeManager)
    {
        MaxHealthModifier = new StatModifier();
        HealthRegenModifier = new StatModifier();
        float healthAndRegenMultiplier = upgradeManager.GetUpgradeHandler("HealthAndRegenUpgrade").UpgradeAmount / 100;
        MaxHealthModifier.IncreaseMultiplier(healthAndRegenMultiplier);
        HealthRegenModifier.IncreaseMultiplier(healthAndRegenMultiplier);

        MovementSpeedModifier = new StatModifier();
        float movementSpeedMultiplier = upgradeManager.GetUpgradeHandler("MovementSpeedUpgrade").UpgradeAmount / 100;
        MovementSpeedModifier.IncreaseMultiplier(movementSpeedMultiplier);

        DamageModifier = new StatModifier();
        KnockbackModifier = new StatModifier();
        float damageAndKnockbackMultiplier = upgradeManager.GetUpgradeHandler("DamageAndKnockbackUpgrade").UpgradeAmount / 100;
        DamageModifier.IncreaseMultiplier(damageAndKnockbackMultiplier);
        KnockbackModifier.IncreaseMultiplier(damageAndKnockbackMultiplier);

        ExperienceGainModifier = new StatModifier();
        float experienceGainMultiplier = upgradeManager.GetUpgradeHandler("ExperienceGainUpgrade").UpgradeAmount / 100;
        ExperienceGainModifier.IncreaseMultiplier(experienceGainMultiplier);

        ExperienceMagnetRangeModifier = new StatModifier();
        float experienceMagnetRangeMultiplier = upgradeManager.GetUpgradeHandler("ExpMagnetRangeUpgrade").UpgradeAmount / 100;
        ExperienceMagnetRangeModifier.IncreaseMultiplier(experienceMagnetRangeMultiplier);
    }
}

