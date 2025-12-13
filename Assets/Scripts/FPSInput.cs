using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput: MonoBehaviour
{
    private PlayerControls _controls;
    private Vector2 _moveInput;

    public float baseSpeed = 6.0f;
    public float speed = 15.0f;
    private float gravity = -9.8f;

    private CharacterController _characterController;

    private void Awake()
    {
        _controls = new PlayerControls();

        _controls.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = _moveInput.x * speed;
        float deltaZ = _moveInput.y * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }
}
