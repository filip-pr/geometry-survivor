using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float baseDamageToCreatures;
    [SerializeField] private float baseDamageToProjectiles;

    public float CreatureDamageMultiplier { get; set; } = 1f;
    public float ProjectileDamageMultiplier { get; set; } = 1f;

    public float DamageToCreatures => baseDamageToCreatures * CreatureDamageMultiplier;
    public float DamageToProjectiles => baseDamageToCreatures * ProjectileDamageMultiplier;

}
