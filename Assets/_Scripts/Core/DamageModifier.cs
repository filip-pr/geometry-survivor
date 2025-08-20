
using UnityEngine;

[System.Serializable]
public class DamageModifier
{
    private float damageMultiplier = 1f;
    private float flatDamageIncrease = 0f;
    private readonly DamageModifier parentModifier = null;

    public float DamageMultiplier
    {
        get
        {
            return damageMultiplier + (parentModifier == null ? parentModifier.DamageMultiplier : 0f);
        }
    }

    public float FlatDamageIncrease { 
        get 
        { 
            return flatDamageIncrease + (parentModifier == null ? parentModifier.FlatDamageIncrease : 0f);
        } 
    }

    public float Modify(float baseDamage)
    {
        return (baseDamage * DamageMultiplier) + FlatDamageIncrease;
    }

    public void IncreaseMultiplier(float amount)
    {
        damageMultiplier += amount;
    }

    public void IncreaseFlatDamage(float amount)
    {
        flatDamageIncrease += amount;
    }
}
