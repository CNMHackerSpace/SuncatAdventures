using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartPickup : MonoBehaviour
{
    [SerializeField] private GameObject dartPopup; // Reference to the dart popup GameObject
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the dart pickup prompt
            dartPopup.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide the dart pickup prompt
            dartPopup.SetActive(false);
        }
    }

    private void Update()
    {
        // Check for right mouse button click to pick up the dart
        if (Input.GetMouseButtonDown(1) && dartPopup.activeSelf)
        {
            // Add the dart to the inventory
            dartInventory.AddDarts(1);
            // Deactivate the dart GameObject
            gameObject.SetActive(false);
            // Check if all darts are picked up, and regenerate them if needed
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
        // Spawn a new dart object at the same position as this object
        Instantiate(dartPrefab, transform.position, Quaternion.identity);
    }
}
