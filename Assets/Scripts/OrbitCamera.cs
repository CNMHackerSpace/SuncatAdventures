using UnityEngine;

// maintains position offset while orbiting around target

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 lookAtAdjustment = new Vector3(0.0f, 0.0f, 0.0f);  // offset from targ;
    private Vector3 offset;
    public float rotSpeed = 1.5f;

    private float rotY;

    private PlayerControls _controls;
    private Vector2 _lookInput;
    public float sensitivity = 1.0f;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        _controls.Player.Look.canceled += ctx => _lookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    // Use this for initialization
    void Start()
    {
        rotY = transform.eulerAngles.y;
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horInput = _lookInput.x * sensitivity * Time.deltaTime;
        if (!Mathf.Approximately(horInput, 0))
        {
            rotY += horInput * rotSpeed;
        }
        else
        {
            rotY += _lookInput.y * sensitivity * Time.deltaTime * rotSpeed * 3;
        }

        Quaternion rotation = Quaternion.Euler(0, rotY, 0);
        transform.position = target.position - (rotation * offset);
        transform.LookAt(target.position + lookAtAdjustment);
    }
}
