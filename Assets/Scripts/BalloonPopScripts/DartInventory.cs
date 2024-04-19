using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartInventory : MonoBehaviour
{
    private const int maxDarts = 15; // Maximum number of darts the player can hold
    private int dartCount = 0;

    // Add darts to the inventory
    public void AddDarts(int amount)
    {
        dartCount += amount;
        // Ensure dart count doesn't exceed the maximum
        dartCount = Mathf.Min(dartCount, maxDarts);
    }

    // Remove darts from the inventory
    public void RemoveDarts(int amount)
    {
        dartCount -= amount;
        // Ensure the dart count doesn't go below zero
        dartCount = Mathf.Max(0, dartCount);
    }

    // Get the current dart count
    public int GetDartCount()
    {
        return dartCount;
    }
}
