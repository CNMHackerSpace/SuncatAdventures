using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartPickup : MonoBehaviour
{
    [SerializeField] private GameObject dartPrefab; // Reference to the dart prefab to spawn
    private DartInventory dartInventory;

    private void Start()
    {
        dartInventory = FindObjectOfType<DartInventory>();
        if (dartInventory == null)
        {
            Debug.LogError("Dart inventory not found in the scene!");
        }
    }

    private void Update()
    {
        // Check for right mouse button click to pick up the dart
        if (Input.GetMouseButtonDown(1))
        {
            // Add the dart to the inventory
            dartInventory.AddDarts(1);
            // Deactivate the dart GameObject
            gameObject.SetActive(false);
            CheckAndRegenerateDarts();
        }
    }

    private void CheckAndRegenerateDarts()
    {
        // Find all inactive dart GameObjects in the scene
        GameObject[] inactiveDarts = GameObject.FindGameObjectsWithTag("Dart");
        if (inactiveDarts.Length == 0)
        {
            // Regenerate darts
            RegenerateDarts();
        }
    }

    private void RegenerateDarts()
    {
        Instantiate(dartPrefab, transform.position, Quaternion.identity);
    }
}
