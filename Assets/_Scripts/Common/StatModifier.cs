
using UnityEngine;

[System.Serializable]
public class StatModifier
{
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private float flatIncrease = 0f;
    private readonly StatModifier parentModifier = null;

    public StatModifier(StatModifier parent = null)
    {
        parentModifier = parent;
    }

    public float Multiplier
    {
        get
        {
            return multiplier + (parentModifier == null ? 0f : parentModifier.Multiplier);
        }
    }

    public float FlatIncrease { 
        get 
        { 
            return flatIncrease + (parentModifier == null ? 0f : parentModifier.FlatIncrease);
        } 
    }

    public float Modify(float baseStat)
    {
        return (baseStat + FlatIncrease) * Multiplier ;
    }

    public void IncreaseMultiplier(float amount)
    {
        multiplier += amount;
    }

    public void IncreaseFlat(float amount)
    {
        flatIncrease += amount;
    }
}
