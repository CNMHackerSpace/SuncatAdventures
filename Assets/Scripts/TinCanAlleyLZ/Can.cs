using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Can : MonoBehaviour
{
    
    private Vector3 home;
    public float maxAllowedDistanceFromHome = 0.25f;
    public new Rigidbody rigidbody;
    public LayerMask mask;

    public bool Standing { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Standing = true;

        // Set origin point
        home = transform.position;
        
        rigidbody = GetComponent<Rigidbody>();

        // Register with game contoller thingy
        TinCanStatus.AddCanToList(gameObject);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(home, transform.position) > maxAllowedDistanceFromHome)
        {
            Standing = false;
        }
        else if (transform.up.y < 0.60f)
        {
            Standing = false;
        }
        else if (transform.up.y > 0.60f)
        {
            Standing = true;
        }
    }

    public void ResetPos()
    {
        rigidbody.rotation = Quaternion.identity;
        rigidbody.position = home;
    }
}
