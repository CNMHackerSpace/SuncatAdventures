using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] string type;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ReactToGrab()
    {
        // Debug.Log(gameObject.name + " has been clicked!");

        // Parent the object to the camera
        gameObject.transform.parent = Camera.main.transform;
        // rigidbody removed
        Destroy(_rigidbody);
    }

    public void ReactToRelease()
    {
        // Debug.Log(gameObject.name + " has been released!");

        // Unparent the object from the camera
        gameObject.transform.parent = null;
        // rigidbody added back
        _rigidbody = gameObject.AddComponent<Rigidbody>();
    }

    public string Type
    {
        get { return type; }
    }
}
