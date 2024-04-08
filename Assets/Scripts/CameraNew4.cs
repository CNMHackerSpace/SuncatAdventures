using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNew4 : MonoBehaviour
{
    public Transform planeTransform;
    public Vector3 offsetThirdPerson;
    public Vector3 offsetFirstPerson;
    public float followSpeed = 5f;
    private Vector3 currentOffset;

    void Start()
    {
        
        currentOffset = offsetThirdPerson;
    }

    public void ToggleView()
    {
        
        currentOffset = (currentOffset == offsetThirdPerson) ? offsetFirstPerson : offsetThirdPerson;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = planeTransform.position + planeTransform.TransformDirection(currentOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, planeTransform.rotation, followSpeed * Time.deltaTime);
    }
}
