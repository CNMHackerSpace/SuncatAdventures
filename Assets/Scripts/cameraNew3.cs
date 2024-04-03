using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraNew3 : MonoBehaviour
{
    public Transform planeTransform; // Assign your plane's transform here in the inspector
    public Vector3 offset; // The offset of the camera from the plane, set this in the inspector
    public float followSpeed = 5f; // How quickly the camera follows the plane

    private void LateUpdate()
    {
        
        Vector3 desiredPosition = planeTransform.position + planeTransform.TransformDirection(offset);

       
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, planeTransform.rotation, followSpeed * Time.deltaTime);

    }
}
