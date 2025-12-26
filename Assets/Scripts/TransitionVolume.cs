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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by " + other.gameObject.name);
        if (other.tag == "Player")
        {
            Debug.Log("Player entered transition volume.");
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
