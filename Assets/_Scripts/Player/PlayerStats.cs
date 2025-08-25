
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public StatModifier MaxHealthModifier { get; private set; } = new StatModifier();
    [field: SerializeField] public StatModifier HealthRegenModifier { get; private set; } = new StatModifier();
    [field: SerializeField] public StatModifier MovementSpeedModifier { get; private set; } = new StatModifier();
    [field: SerializeField] public StatModifier DamageModifier { get; private set; } = new StatModifier();
    [field: SerializeField] public StatModifier KnockbackModifier { get; private set; } = new StatModifier();
    [field: SerializeField] public StatModifier AttackSpeedModifier { get; private set; } = new StatModifier();
    [field: SerializeField] public StatModifier ExperienceGainModifier { get; private set ; } = new StatModifier();
    [field: SerializeField] public StatModifier ExperienceMagnetRangeModifier { get; private set; } = new StatModifier();
}

