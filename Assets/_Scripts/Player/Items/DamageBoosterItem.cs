using UnityEngine;

public class DamageBoosterItem : PlayerItem
{
    public override string ItemName => "Damage Booster";

    public override int MaxLevel => 5;

    private StatModifier damageModifier;

    public override void SetupModifiers(PlayerStats playerStats)
    {
        damageModifier = playerStats.DamageModifier;
    }

    protected override void OnLevelUp()
    {
        switch (Level)
        {
            case 1:
                damageModifier.IncreaseMultiplier(0.125f);
                break;
            case 2:
                damageModifier.IncreaseMultiplier(0.25f);
                break;
            case 3:
                damageModifier.IncreaseMultiplier(0.5f);
                break;
            case 4:
                damageModifier.IncreaseMultiplier(1f);
                break;
            case 5:
                damageModifier.IncreaseMultiplier(2f);
                break;
        }
    }
}

