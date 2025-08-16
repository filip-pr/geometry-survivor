using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float baseDamage = 0.2f;
    [SerializeField] private float baseKnockback = 5f;

    public float DamageMultiplier { get; set; } = 1f;
    public float KnockbackMultiplier { get; set; } = 1f;

    public float Damage => baseDamage * DamageMultiplier;
    public float Knockback => baseKnockback * KnockbackMultiplier;

}
