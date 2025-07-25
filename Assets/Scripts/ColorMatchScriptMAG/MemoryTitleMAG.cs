// Programmer: Mark Gonzales
// Date: 2025-07-24
// Description: Memory Tile script for a color matching game using Unity's XR Interaction Toolkit.
// This script handles tile selection, color matching, and resetting tiles after a delay.

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MemoryTile : XRBaseInteractable
{
    public Color tileColor;
    private Renderer rend;
    private static MemoryTile firstTile;
    private static bool lockInput = false;

    protected override void Awake()
    {
        base.Awake();
        rend = GetComponent<Renderer>();
        selectEntered.AddListener(HandleSelect); // Unity 6-safe
    }

    private void HandleSelect(SelectEnterEventArgs args)
    {
        if (lockInput || rend.material.color == Color.black) return;

        rend.material.color = tileColor;

        if (firstTile == null)
        {
            firstTile = this;
        }
        else
        {
            if (firstTile.tileColor == tileColor && firstTile != this)
            {
                firstTile.rend.material.color = Color.black;
                rend.material.color = Color.black;
            }
            else
            {
                lockInput = true;
                Invoke(nameof(ResetTiles), 1.5f);
            }
            firstTile = null;
        }
    }

    private void ResetTiles()
    {
        foreach (var tile in FindObjectsByType<MemoryTile>(FindObjectsSortMode.None))
        {
            if (tile.rend.material.color != Color.black)
                tile.rend.material.color = Color.white;
        }
        lockInput = false;
    }

    protected new void OnDestroy()
    {
        selectEntered.RemoveListener(HandleSelect);
    }
}
