using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootBall : MonoBehaviour
{

    private Camera _camera;
    [SerializeField] private GameObject BasketballPrefab;
    private GameObject _basketball;

    public float throwForce = 10f;
    public float throwUpForce = 3f;

    private float startHold;
    private float maxHoldTime = 2f;

    // Start is called before the first frame update
    void Start()
    {

        _camera = GetComponent<Camera>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Change force with left click hold
        if (Input.GetMouseButtonDown(0))
        {
            startHold = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            float holdDownTime = Time.time - startHold;

            if (holdDownTime > maxHoldTime)
            {
                holdDownTime = maxHoldTime;
            }

            throwForce = throwForce * holdDownTime;

            if (_basketball == null)
            {
                _basketball = Instantiate(BasketballPrefab) as GameObject;
                _basketball.transform.position = transform.TransformPoint(Vector3.forward * 2f);
                Throw();

            }

            else
            {
                Destroy(_basketball);

                _basketball = Instantiate(BasketballPrefab) as GameObject;
                _basketball.transform.position = transform.TransformPoint(Vector3.forward * 2f);
                Throw();
            }

            throwForce = 10;
            throwUpForce = 3;

        }

        //Change angle with Right Mouse Click
        if (Input.GetMouseButtonDown(1))
        {
            startHold = Time.time;
        }

        if (Input.GetMouseButtonUp(1))
        {
            float holdDownTime = Time.time - startHold;

            if (holdDownTime > maxHoldTime)
            {
                holdDownTime = maxHoldTime;
            }

            throwUpForce = throwUpForce * holdDownTime;

        }
    }

    private void Throw()
    {
        Rigidbody basketballRB = _basketball.GetComponent<Rigidbody>();

        Vector3 forceToAdd = _camera.transform.forward * throwForce + transform.up * throwUpForce;

        basketballRB.AddForce(forceToAdd, ForceMode.Impulse);
    }
}
