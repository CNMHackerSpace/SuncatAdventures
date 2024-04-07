using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float FlySpeed = 5;
    public float YawAmount = 120;
    private float Yaw;
    public AudioSource audioSource; // Reference to the AudioSource component
    public float activationSpeed = 5f; // Speed to start playing the sound
    public float deactivationSpeed = 5f; // Speed to stop playing the sound
   
    void Start()
    {
        // AudioSource component is attached to the GameObject
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()

    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            FlySpeed = FlySpeed + 5f;
            CheckAudio();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            FlySpeed = FlySpeed - 5f;
            CheckAudio();
        }
        transform.position += transform.forward * FlySpeed * Time.deltaTime;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Yaw += horizontalInput * YawAmount * Time.deltaTime;
        float pitch = Mathf.Lerp(0, 20, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0, 30, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);
        transform.rotation = Quaternion.Euler(Vector3.up * Yaw + Vector3.right * pitch + Vector3.forward * roll);

    }
    //Check speed for audio auto turn on and off
    void CheckAudio()
    {
        if (FlySpeed >= activationSpeed && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (FlySpeed < deactivationSpeed && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    
    
}
