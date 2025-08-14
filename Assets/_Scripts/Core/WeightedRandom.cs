using System.Collections.Generic;
using UnityEngine;

public interface IWeightedItem
{
    public float Weight { get; }
}

public static class WeightedRandom
{
    public static T Choose<T>(IEnumerable<T> items) where T : IWeightedItem // TODO potentially optimize with binary search
    {
        float totalWeight = 0f;
        foreach (var item in items)
        {
            totalWeight += item.Weight;
        }
        if (totalWeight < 0f)
        {
            return default;
        }
        float choicePoint = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;
        foreach (var item in items)
        {
            cumulativeWeight += item.Weight;
            if (cumulativeWeight >= choicePoint)
            {
                return item;
            }
        }
        return default;
    }
}
