using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneCamera : MonoBehaviour
{
    //public Transform target; // The target the camera should follow (your airplane)
    //public Vector3 offset; // The offset at which the camera should follow the target
    //public float smoothSpeed = 0.125f; // The speed at which the camera should follow the target

    //void FixedUpdate()
    //{
    //    Vector3 desiredPosition = target.position + offset;
    //    Quaternion rotation= Quaternion.Euler(0,target.transform.rotation.y,0);
    //    Vector3 smoothedPosition = Vector3.Lerp(transform.position,rotation * desiredPosition,  smoothSpeed * Time.deltaTime);
    //    transform.position = smoothedPosition;

    //    transform.LookAt(target);
    //}
    [SerializeField] Transform target;
    [SerializeField] Vector3 pointAtOffset;
    public float rotSpeed = 1.5f;

    private float rotY;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        rotY = transform.eulerAngles.y;
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
        rotY = target.rotation.y;

        Quaternion rotation = Quaternion.Euler(0, rotY, 0);
        transform.position =target.position -(rotation * offset);
        transform.LookAt(target.position + pointAtOffset);
    }
}