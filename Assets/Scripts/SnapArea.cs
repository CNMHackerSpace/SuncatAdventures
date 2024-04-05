using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapArea : MonoBehaviour
{
    // When a reactivetarget of type motherboard enters the snap area, it should snap to the area
    [SerializeField] string type;
    ReactiveTarget target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        target = other.GetComponent<ReactiveTarget>();
        if (target != null && this.type == target.Type)
        {
            // Snap the object to the area
            target.gameObject.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
        }
    }

    // Get type
    public string Type
    {
        get { return type; }
    }
}
