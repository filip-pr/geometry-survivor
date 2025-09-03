
using UnityEngine;

/// <summary>
/// Class representing a generic stat modifier with multiplier and flat increase.
/// </summary>
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

    /// <summary>
    /// Modify the stat by applying flat increase and the multiplier.
    /// </summary>

    public float Modify(float baseStat)
    {
        return (baseStat + FlatIncrease) * Multiplier ;
    }

    /// <summary>
    /// Increase the multiplier modifier.
    /// </summary>
    public void IncreaseMultiplier(float amount)
    {
        multiplier += amount;
    }

    /// <summary>
    /// Increase the flat increase modifier.
    /// </summary>
    public void IncreaseFlat(float amount)
    {
        flatIncrease += amount;
    }
}
