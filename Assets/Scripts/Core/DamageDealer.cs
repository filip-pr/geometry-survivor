using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float baseEnemyDamage = 0.2f;
    [SerializeField] private float baseProjectileDamage = 1f;

    public float EnemyDamageMultiplier { get; set; } = 1f;
    public float ProjectileDamageMultiplier { get; set; } = 1f;

    public float EnemyDamage => baseEnemyDamage * EnemyDamageMultiplier;
    public float ProjectileDamage => baseProjectileDamage * ProjectileDamageMultiplier;

}
