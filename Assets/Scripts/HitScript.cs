using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    public GameObject particleEffectPrefab; // Assign your particle effect prefab here

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy Plane") // Replace "Plane" with the tag of your plane prefab
        {
            // Instantiate the particle effect at the point of collision and play it
            GameObject effect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f); // Destroy the particle effect after 2 seconds

            // Optionally, destroy the bullet as well
            Destroy(gameObject);
        }
    }
}
