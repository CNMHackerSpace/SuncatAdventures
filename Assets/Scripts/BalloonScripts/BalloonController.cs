using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public float windDirectionChangeAltitude = 200.0f;
    public float windForce = 20.0f;
    public Vector3 groundWindDirection = new Vector3(1, 0, 0);
    private ConstantForce force;

    public float balloonDensity = 1.204f; //kg/m^3 start same density as air

    public Buoyancy buoyancy;

    // Start is called before the first frame update
    void Start()
    {
        force = GetComponent<ConstantForce>();
        buoyancy = new Buoyancy();
        buoyancy.ObjectDensity = balloonDensity; //kg/m^3
        buoyancy.ObjectVolume = 0.5f; //m^3
    }

    // Update is called once per frame
    void Update()
    {
        var yForce = buoyancy.BuoyancyForce;
        
        force.force = new Vector3(0, yForce, 0);
    }

    private float CalculateWindForce(float altitude)
    {
        return windForce * (1 - altitude / windDirectionChangeAltitude);
    }
}
