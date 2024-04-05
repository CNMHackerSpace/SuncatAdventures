using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIplaneflight : MonoBehaviour
{
    public float FlySpeed=10f;
    public float YawAmount = 120;
    private float Yaw;
   

    // Variables for the RandomFlight coroutine
    private float targetPitch;
    private float targetYaw;
    private float targetRoll;
    private float smoothTime = 2f;
    public float minX = -400f;
    public float maxX = 400f;
    public float minY = 10f; // Minimum altitude
    public float maxY = 150f; // Maximum altitude
    public float minZ = -400f;
    public float maxZ = 400f;

   

    void Start()
    {
        FlySpeed = 10f;
        // Start the random flight coroutine
        StartCoroutine(RandomFlight());
    }

    void Update()
    {
        // Constrain the object's position within the defined boundaries
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);

    }

    // Random flight coroutine
    private IEnumerator RandomFlight()
    {
        float levelOutTime = .1f; // Time to level out after a turn
        float maxTurnRate = 60f; // Maximum degrees per second the plane can turn

        while (true)
        {
            // Generate random target values for pitch and roll within a smaller range for smoother transitions
            targetPitch = Random.Range(-20f, 20f);
            
            targetRoll = Random.Range(-20f, 20f);

            // Calculate the time to reach the next random input
            float timeToNextInput = Random.Range(3f, 7f);

            // Set a random target yaw for each turn
            float targetYawChange = Random.Range(-YawAmount, YawAmount);

            float startTime = Time.time;
            float startYaw = Yaw;
            // Interpolate from current to target values over the specified time
            
            while (Time.time - startTime < timeToNextInput)
            {
                float t = (Time.time - startTime) / timeToNextInput;
                float pitch = Mathf.LerpAngle(transform.eulerAngles.x, targetPitch, t);
                Yaw = Mathf.LerpAngle(startYaw, startYaw + targetYawChange, t); // Gradually interpolate the yaw
                float roll = Mathf.LerpAngle(transform.eulerAngles.z, targetRoll, t);

                // Apply the interpolated rotation
                transform.rotation = Quaternion.Euler(pitch, Yaw, roll);

                // Move the object forward at an increased speed
                transform.position += transform.forward * FlySpeed * Time.deltaTime;

                yield return null;
            }

            // Level out the plane after the turn
            startTime = Time.time;
            float startPitch = transform.eulerAngles.x;
            float startRoll = transform.eulerAngles.z;
            while (Time.time - startTime < levelOutTime)
            {
                float t = (Time.time - startTime) / levelOutTime;
                float pitch = Mathf.LerpAngle(startPitch, 0, t);
                float roll = Mathf.LerpAngle(startRoll, 0, t);

                // Apply the leveling rotation
                transform.rotation = Quaternion.Euler(pitch, Yaw, roll);

                // Continue moving the object forward
                transform.position += transform.forward * FlySpeed * Time.deltaTime;

                yield return null;
            }
            
        }
    }



}
