using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float FlySpeed = 5;
    public float YawAmount = 120;
    private float Yaw;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
        
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            FlySpeed = FlySpeed + 5f;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            FlySpeed = FlySpeed - 5f;
        }
        transform.position += transform.forward * FlySpeed * Time.deltaTime;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Yaw += horizontalInput * YawAmount * Time.deltaTime;
        float pitch = Mathf.Lerp(0, 20, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll= Mathf.Lerp(0,30,Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);
        transform.rotation = Quaternion.Euler(Vector3.up * Yaw + Vector3.right * pitch+Vector3.forward * roll);
        
    }
}
