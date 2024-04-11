using UnityEngine;
using UnityEngine.UI; // Required for working with UI elements like Button

public class ParticleToggle : MonoBehaviour
{
    public ParticleSystem particleSystem; // Assign this in the Inspector
    public Button toggleButton; // Assign this in the Inspector

    void Start()
    {
        // Add a listener to the button to call the ToggleParticleSystem function when clicked
        toggleButton.onClick.AddListener(ToggleParticleSystem);
    }

    void ToggleParticleSystem()
    {
        // Check if the particle system is playing
        if (particleSystem.isPlaying)
        {
            particleSystem.Stop(); // Stop the particle system if it's playing
        }
        else
        {
            particleSystem.Play(); // Play the particle system if it's not playing
        }
    }
}
