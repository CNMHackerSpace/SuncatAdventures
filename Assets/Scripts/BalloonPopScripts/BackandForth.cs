using UnityEngine;

public class BackandForth : MonoBehaviour
{
    public float speed = 10.0f; // Default speed
    public float maxY = 60f;    // Default maxY
    public float minY = -20f;   // Default minY

    private int _direction = 1;

    void Update()
    {
        transform.Translate(0, _direction * speed * Time.deltaTime, 0);
        bool bounced = false;
        if (transform.position.y > maxY || transform.position.y < minY)
        {
            _direction = -_direction;
            bounced = true;
        }
        if (bounced)
        {
            transform.Translate(0, _direction * speed * Time.deltaTime, 0);
        }
    }
}
