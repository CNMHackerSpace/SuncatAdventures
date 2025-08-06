using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ArcadeMachineInteraction : MonoBehaviour
{
    [Header("XR Game Activation")]
    public GameObject gameCanvas;
    public InputActionProperty activationInput;  // Assign XR trigger or button input

    [Header("Interaction Prompt")]
    public GameObject interactionPromptObject;
    public TextMeshProUGUI interactionPromptText;

    private bool isPlayerInRange = false;

    private void OnEnable()
    {
        activationInput.action.Enable();
    }

    private void OnDisable()
    {
        activationInput.action.Disable();
    }

    void Update()
    {
        if (isPlayerInRange && activationInput.action.WasPressedThisFrame())
        {
            gameCanvas.SetActive(true);
            interactionPromptObject?.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionPromptObject != null && interactionPromptText != null)
            {
                interactionPromptText.text = $"Press Button to Play";
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
