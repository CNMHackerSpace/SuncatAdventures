using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    private bool isAlive;

    public const float baseSpeed = 3.0f; // Base speed for the AI
    public float speed = 3.0f;                                   
    public float obstacleRange = 5.0f;

    [SerializeField] private GameObject PaintballPrefab; // Reference to the lavaball prefab

    private float lastFireTime = 0f;
    public float fireCooldown = 1f;

    void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            float detectionRange = 100f; // Set this to your desired detection distance

            if (Physics.SphereCast(ray, 1.5f, out hit, detectionRange))
            {
                GameObject hitObject = hit.transform.gameObject;
                Debug.Log("AI hit: " + hitObject.name + " Tag: " + hitObject.tag); // Debug log for troubleshooting
                if (hitObject.CompareTag("Player"))
                {
                    // Look at the player
                    Vector3 directionToPlayer = (hitObject.transform.position - transform.position).normalized;
                    directionToPlayer.y = 0;
                    if (directionToPlayer != Vector3.zero)
                        transform.rotation = Quaternion.LookRotation(directionToPlayer);

                    // Damage player
                    FindObjectOfType<PlayerStats>().TakeDamage(10);

                    // Fire paintball with cooldown
                    if (Time.time - lastFireTime > fireCooldown)
                    {
                        Debug.Log("AI fires a paintball!");
                        GameObject paintball = Instantiate(PaintballPrefab);
                        paintball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        paintball.transform.rotation = transform.rotation;

                        Rigidbody rb = paintball.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            float fireForce = 1500f;
                            rb.AddForce(transform.forward * fireForce);
                        }
                        lastFireTime = Time.time;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }        
    }

    public void SetAlive(bool alive)
    {                     
        isAlive = alive;
    }

    //May enable enemy speed changes in the future
    //void OnEnable()
    //{
    //    Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    //}
    //void OnDisable()
    //{
    //    Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    //}

    //private void OnSpeedChanged(float value)
    //{    
    //    speed = baseSpeed * value;
    //}
}