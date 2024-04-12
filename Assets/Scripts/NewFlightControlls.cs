using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float FlySpeed = 5;
    public float YawAmount = 120;
    private float Yaw;
    public AudioSource audioSource; // Reference to the AudioSource component
    public float activationSpeed = 10f; // Speed to start playing the sound
    public float deactivationSpeed = 5f; // Speed to stop playing the sound
    // Start is called before the first frame update
    void Start()
    {
        // Ensure the AudioSource component is attached to the GameObject
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
        var oldRot = transform.rotation;
        transform.rotation = Quaternion.Euler(Vector3.up * Yaw + Vector3.right * pitch + Vector3.forward * roll);
        if(oldRot.x - transform.rotation.x > 0.1f)
        {
           Debug.Log($"Pitch{transform.rotation.x}");
        }
    }
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
    // Call this method from your UI button to toggle sound manually
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
