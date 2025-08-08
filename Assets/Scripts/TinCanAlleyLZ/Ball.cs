using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Ball : MonoBehaviour
{
    private Vector3 home;
    private new Rigidbody rigidbody;
    private XRGrabInteractable interactable;
    public bool Thrown { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set origin point
        home = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        interactable = GetComponent<XRGrabInteractable>();

        Thrown = false;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Thrown = true;
        }
    }

    void LateUpdate()
    {
        // OOB ball return
        if (transform.position.y < 0)
        {
            Invoke("ResetPos", 1.5f);
        }

        if (Thrown)
        {
            interactable.interactionLayers = InteractionLayerMask.GetMask("Nothing");
        }
        else
        {
            interactable.interactionLayers = InteractionLayerMask.GetMask("Default");
        }
    }

    public void ResetPos()
    {
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.rotation = Quaternion.identity;
        rigidbody.position = home;

        Thrown = false;
    }
}
