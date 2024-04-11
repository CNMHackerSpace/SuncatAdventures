using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f; // The constant speed of the bullet
    public float lifespan = 5f; // The lifespan of the bullet in seconds

    void Start()
    {
        NewBehaviourScript planeSpeed = GameObject.Find("BluePlane").GetComponent<NewBehaviourScript>();

        // Get the flySpeed from the plane
        float flySpeed = planeSpeed.FlySpeed;


        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Ensure gravity is turned off.
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Freeze rotation on all axes
        rb.velocity = transform.forward * (bulletSpeed + flySpeed); // Apply velocity in the local forward direction
        Invoke("DestroyBullet", lifespan); // Schedule destruction of the bullet
    }

    void DestroyBullet()
    {
        Destroy(gameObject); // Destroy the bullet
    }
}
