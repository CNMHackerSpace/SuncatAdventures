using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionVolume : MonoBehaviour
{
    public string levelToLoad;
    private Vector3 _volumePosition;
    // Start is called before the first frame update
    void Start()
    {
                 // Get the position of the volume
            _volumePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // Get the player's position
            Vector3 playerPosition = other.transform.position;

            // Get the distance between the player and the volume
            float distance = Vector3.Distance(playerPosition, _volumePosition);

            // Get the volume's scale
            Vector3 volumeScale = transform.localScale;

            // Get the volume's size
            float volumeSize = volumeScale.x;

            // If the player is inside the volume
            if (distance < volumeSize / 2)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
}
