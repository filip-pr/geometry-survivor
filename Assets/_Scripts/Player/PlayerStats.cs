
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public StatModifier MaxHealthModifier { get; set; }
    [field: SerializeField] public StatModifier HealthRegenModifier { get; set; }
    [field: SerializeField] public StatModifier MovementSpeedModifier { get; set; }
    [field: SerializeField] public StatModifier DamageModifier { get; set; }
    [field: SerializeField] public StatModifier KnockbackModifier { get; set; }
    [field: SerializeField] public StatModifier AttackSpeedModifier { get; set; }
    [field: SerializeField] public StatModifier ExperienceGainModifier { get; set ; }
    [field: SerializeField] public StatModifier ExperienceMagnetRangeModifier { get; set; }

    public void SetUpgradeModifiers(UpgradeManager upgradeManager)
    {

    }

}

