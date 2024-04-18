using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapArea : MonoBehaviour
{
    // When a reactivetarget of type motherboard enters the snap area, it should snap to the area
    [SerializeField] string type;
    ReactiveTarget target;
    bool snapped = false;

    private void OnTriggerEnter(Collider other)
    {
        target = other.GetComponent<ReactiveTarget>();
        if (target != null && this.type == target.Type && !snapped)
        {
            // Snap the object to the area
            target.gameObject.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
            snapped = true;
        }
    }
    
    public string Type { get => type;}
}
