//using UnityEngine;

//public class PaintballShooter : MonoBehaviour
//{
//    public GameObject paintballPrefab;
//    public float shootForce = 700f;

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0)) // Replace with XR input if needed
//        {
//            ShootPaintball();
//        }
//    }

//    void ShootPaintball()
//    {
//        GameObject paintball = Instantiate(
//            paintballPrefab,
//            transform.position + transform.forward, // In front of camera/player
//            transform.rotation);

//        Rigidbody rb = paintball.GetComponent<Rigidbody>();
//        if (rb != null)
//        {
//            rb.AddForce(transform.forward * shootForce);
//        }
//    }
//}

//Version for XR Interaction Toolkit

using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem; // Ensure you have the Input System package installed

public class PaintballShooter : MonoBehaviour
{
    public GameObject paintballPrefab;
    public float shootForce = 3000f;
    public InputActionReference shootAction; // Assign in Inspector

    void OnEnable()
    {
        shootAction.action.Enable();
    }

    void OnDisable()
    {
        shootAction.action.Disable();
    }

    void Update()
    {
        if (shootAction.action.WasPressedThisFrame())
        {
            ShootPaintball();
        }
    }

    void ShootPaintball()
    {
        GameObject paintball = Instantiate(
            paintballPrefab,
            transform.position + transform.forward,
            transform.rotation);

        Rigidbody rb = paintball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * shootForce);
        }
    }
}