using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundOnOff : MonoBehaviour
{
    public AudioSource audioSource;

    public void ToggleSound()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.Play();
            }
        }
    
}
