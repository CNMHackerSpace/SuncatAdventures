using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class BalloonPopFPSInput : MonoBehaviour
{
    public float speed = 5.0f;
    public float gravity = -9.8f;

    private CharacterController _charController;

    
    void Start()
    {
       
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        movement *= Time.deltaTime * (speed * speed);
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }

}
