using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{

    public Image throwBar;
    public Image curveBar;
    public float throwCharge;
    public float curveCharge;

    private float lholdDownTime = 0.0f;
    float rholdDownTime = 0.0f;

    private float maxHoldTime = 2f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lholdDownTime = Mathf.Clamp(lholdDownTime,0,maxHoldTime);
           
            throwBar.fillAmount = lholdDownTime/maxHoldTime;
        }

        if (Input.GetMouseButtonDown(1))
        {
            rholdDownTime = Mathf.Clamp(rholdDownTime, 0, maxHoldTime);

            curveBar.fillAmount = rholdDownTime/maxHoldTime;
        }

        if (Input.GetMouseButtonUp(0)) 
        {

            throwBar.fillAmount = 0;

            curveBar.fillAmount = 0;
        }

    }

   


}
