using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip clip1, clip2; // Assign these in the Inspector
    private AudioSource audioSource1, audioSource2;
    private bool isClip1Playing = false, isClip2Playing = false;
    public Button button1, button2; // Assign button references in the Inspector

    void Start()
    {
        // Add AudioSource components and assign clips
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1.clip = clip1;
        audioSource1.playOnAwake = false;
        audioSource1.loop = true;

        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.clip = clip2;
        audioSource2.playOnAwake = false;
        audioSource2.loop = true;

        // Add listeners for button clicks
        button1.onClick.AddListener(() => ToggleClip(audioSource1, ref isClip1Playing));
        button2.onClick.AddListener(() => ToggleClip(audioSource2, ref isClip2Playing));
    }

    void ToggleClip(AudioSource audioSource, ref bool isClipPlaying)
    {
        // Toggle the play state based on the isClipPlaying flag
        if (isClipPlaying)
        {
            audioSource.Stop();
            isClipPlaying = false;
        }
        else
        {
            audioSource.Play();
            isClipPlaying = true;
        }
    }
}