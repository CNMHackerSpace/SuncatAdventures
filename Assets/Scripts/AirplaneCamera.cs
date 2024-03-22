using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneCamera : MonoBehaviour
{
    public Transform target; // The target the camera should follow (your airplane)
    public Vector3 offset; // The offset at which the camera should follow the target
    public float smoothSpeed = 0.125f; // The speed at which the camera should follow the target

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}