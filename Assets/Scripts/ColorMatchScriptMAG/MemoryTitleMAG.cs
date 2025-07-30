using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MemoryTileMAG : XRBaseInteractable
{
    public Color tileColor; // Assigned per tile in Inspector
    public Color defaultColor = Color.gray; // Color when hidden

    public MemoryGameManagerMAG gameManager;  // Reference to the manager (assign in inspector)

    private Renderer rend;
    private static MemoryTileMAG firstTile;   // Shared across all tiles
    private static bool lockInput = false;    // Prevents rapid input spamming

    protected override void Awake()
    {
        base.Awake();
        rend = GetComponent<Renderer>();
        rend.material.color = defaultColor;
        selectEntered.AddListener(HandleSelect);
    }

    private void HandleSelect(SelectEnterEventArgs args)
    {
        if (lockInput || rend == null || !gameObject.activeSelf)
            return;

        StartCoroutine(SelectTile());
    }

    private IEnumerator SelectTile()
    {
        if (firstTile == null)
        {
            rend.material.color = tileColor;
            firstTile = this;
        }
        else
        {
            rend.material.color = tileColor;
            lockInput = true;

            yield return new WaitForSeconds(1.5f);

            if (ColorsAreEqual(firstTile.tileColor, tileColor) && firstTile != this)
            {
                firstTile.gameObject.SetActive(false);
                gameObject.SetActive(false);

                // Notify manager to check for win
                if (gameManager != null)
                    gameManager.CheckForWin();
            }
            else
            {
                firstTile.rend.material.color = defaultColor;
                rend.material.color = defaultColor;
            }

            firstTile = null;
            lockInput = false;
        }
    }

    private bool ColorsAreEqual(Color a, Color b, float tolerance = 0.01f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }

    public void ProcessSelection()
    {
        HandleSelect(null);
    }

    public void ResetTile()
    {
        gameObject.SetActive(true);
        rend.material.color = defaultColor;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        selectEntered.RemoveListener(HandleSelect);
    }
}
