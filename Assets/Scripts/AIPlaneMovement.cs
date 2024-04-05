using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlaneMovement : MonoBehaviour
{
    public float FlySpeed = 5;
    public float YawAmount = 120;
    public float PitchRate = 2;
    public float RollRate = 2;
    public float RotationSpeed = 10;
    
    // Parameters for random movement
    public Vector2 speedRange = new Vector2(5f, 25f);
    public Vector2 altitudeRange = new Vector2(10f, 150f);
    public Vector2 areaBounds = new Vector2(400f, -400f); // Assuming a square area for simplicity

    private Vector3 targetPosition;

    void Start()
    {       
        SetRandomTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        FlyTowardsTarget();
        // Check if the plane has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            SetRandomTargetPosition();
        }
    }

    void FlyTowardsTarget()
    {
        // Calculate the direction to the target position
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move the plane towards the target position
        transform.position += direction * FlySpeed * Time.deltaTime;

        // Calculate the target rotation to look at the target position
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Calculate the pitch and roll for the target rotation
        float pitch = Vector3.Dot(transform.up, Vector3.Cross(direction, transform.forward));
        float roll = Vector3.Dot(transform.right, direction) * -1;

        // Adjust the target rotation to include pitch and roll
        targetRotation *= Quaternion.Euler(pitch, 0, roll);

        // Rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * YawAmount);

        
    }

    void SetRandomTargetPosition()
    {
        // Generate a random position within the specified bounds
        targetPosition = new Vector3(
            Random.Range(-areaBounds.x, areaBounds.x),
            Random.Range(altitudeRange.x, altitudeRange.y),
            Random.Range(-areaBounds.y, areaBounds.y)
        );

        // Set a random speed within the specified range
        FlySpeed = Random.Range(speedRange.x, speedRange.y);
    }

}
