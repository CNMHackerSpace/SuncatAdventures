using UnityEngine;

public class Paintball : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject.GetComponent<ReactiveTarget1>();
        if (target != null)
        {
            target.ReactToHit(); // Or your knock-out logic
            FindObjectOfType<PlayerStats>().AddPoints(10);
        }
        Destroy(gameObject);
    }
}
