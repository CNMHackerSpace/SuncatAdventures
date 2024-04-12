using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public float windDirectionChangeAltitude = 200.0f;
    public float windForce = 20.0f;
    public Vector3 groundWindDirection = new Vector3(1, 0, 0);
    public float windSpeed = 10.0f;
    private ConstantForce force;
    private Rigidbody rb;
    private float yForce = 0.0f;

    public TMP_Text altitudeText;
    public TMP_Text windSpeedText;
    
    public float upForce = 0.1f; //kg/m^3
    public float downForce = 0.1f; //kg/m^3
    public float maxYForce = 20.0f; //kg/m^3
    public float minYForce = -10.0f; //kg/m^3
    
    public Buoyancy buoyancy;

    // Start is called before the first frame update
    void Start()
    {
        force = GetComponent<ConstantForce>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // balloonDensity += Input.GetKey(KeyCode.W) ? upDensityChange : Input.GetKey(KeyCode.S) ? -downDensityChange : 0;
        // float altitude = transform.position.y;
        // buoyancy.fluidDensity = (MAX_ALTITUDE - altitude)/MAX_ALTITUDE * 1.204f; //kg/m^3
        // buoyancy.objectDensity = balloonDensity; //kg/m^3
        
        if (Input.GetKey(KeyCode.W))
        {
            yForce += upForce;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            yForce -= downForce;
        }
        
        var windForce = CalculateWindForce(transform.position.y);
        force.relativeForce = new Vector3(windForce.x, yForce, windForce.z);
    }

    private Vector3 CalculateWindForce(float altitude)
    {
        altitudeText.text = $"Altitude: {altitude.ToString("F2")}";
        var altidudeRatio = (windDirectionChangeAltitude-altitude) / windDirectionChangeAltitude;
        altidudeRatio = Math.Clamp(altidudeRatio, -1, 1);

        var altitudeWindSpeed = windSpeed * altidudeRatio;
        windSpeedText.text = $"Wind speed: {altitudeWindSpeed.ToString("F2")}";

        var relativeSpeedRatio = (rb.velocity.magnitude - altitudeWindSpeed)/altitudeWindSpeed;
        relativeSpeedRatio = Math.Clamp(relativeSpeedRatio, -1, 1);

        return relativeSpeedRatio * groundWindDirection * windForce;
    }
}
