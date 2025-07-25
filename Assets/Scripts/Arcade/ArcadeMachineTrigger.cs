using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArcadeMachineInteraction : MonoBehaviour
{
    public GameObject gameCanvas;
    public string gameName = "Arcade Game";
    public TetrominoSpawner spawner;

    [Header("Interaction Prompt")]
    public GameObject interactionPromptObject;
    public TextMeshProUGUI interactionPromptText; // or use UnityEngine.UI.Text

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            gameCanvas.SetActive(true);
            spawner?.SpawnNewTetromino();
            interactionPromptObject?.SetActive(false); // hide prompt when entering game
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionPromptObject != null && interactionPromptText != null)
            {
                interactionPromptText.text = $"Press E to Play {gameName}";
                interactionPromptObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionPromptObject != null)
            {
                interactionPromptObject.SetActive(false);
            }
        }
    }
}
