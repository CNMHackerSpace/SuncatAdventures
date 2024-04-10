using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNew4 : MonoBehaviour
{
    public Transform planeTransform; // Reference to the plane's transform
    public Vector3 offsetThirdPerson; // Offset for third-person view
    public Vector3 offsetFirstPerson; // Offset for first-person view
    public float followSpeed = 5f; // Speed at which the camera follows the plane
    private Vector3 currentOffset; // Current offset based on the camera view
    private bool isFirstPersonView = false; // Flag to check if it's first person view

    void Start()
    {
        // Set initial offset to third person
        currentOffset = offsetThirdPerson;
    }

    public void ToggleView()
    {
        // Toggle the view flag and update the current offset
        isFirstPersonView = !isFirstPersonView;
        currentOffset = isFirstPersonView ? offsetFirstPerson : offsetThirdPerson;
    }

    void LateUpdate()
    {
        if (isFirstPersonView)
        {
            // For first person, lock the camera to the plane's position plus offset
            transform.position = planeTransform.position + planeTransform.TransformDirection(offsetFirstPerson);
            transform.rotation = planeTransform.rotation;
        }
        else
        {
            // For third person, interpolate the position
            Vector3 desiredPosition = planeTransform.position + planeTransform.TransformDirection(offsetThirdPerson);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, planeTransform.rotation, followSpeed * Time.deltaTime);
        }
    }
}
