using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public GameObject balloon;
    public float balloonDensity = 1.204f; //kg/m^3 start same density as air


    public Buoyancy buoyancy;

    // Start is called before the first frame update
    void Start()
    {
        buoyancy = new Buoyancy();
        buoyancy.ObjectDensity = balloonDensity; //kg/m^3
        buoyancy.ObjectVolume = 0.5f; //m^3

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
