using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IWeightedItem
{
    public float Weight { get; }
}

/// <summary>
/// Static utility class for weighted random selection.
/// </summary>
public static class WeightedRandom
{
    /// <summary>
    /// Choose a random item weighted by it's weight.
    /// </summary>
    public static T Choose<T>(IEnumerable<T> items) where T : IWeightedItem
    {
        float totalWeight = 0f;
        foreach (var item in items)
        {
            totalWeight += item.Weight;
        }
        if (totalWeight <= 0f)
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

    /// <summary>
    /// Choose up to N unique random items weighted by their weight.
    /// </summary>
    public static List<T> ChooseN<T>(IEnumerable<T> items, int n) where T : IWeightedItem
    {
        List<T> chosenItems = new List<T>();
        List<T> itemsCopy = items.ToList();
        for (int i = 0; i < n; i++)
        {
            T chosenItem = Choose(items);
            if (chosenItem == null) break;
            chosenItems.Add(chosenItem);
            itemsCopy.Remove(chosenItem);
        }
        return chosenItems;
    }
}
