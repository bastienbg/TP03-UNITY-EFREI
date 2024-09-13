using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbite : MonoBehaviour
{
    public Transform target;      
    public float distance = 5.0f; 
    public float zoomMin = 2.0f;  
    public float zoomMax = 10.0f; 
    public float zoomSpeed = 2.0f;    
    public float xSpeed = 120.0f;     
    public float ySpeed = 80.0f;      
    public float yMinLimit = -20f;    
    public float yMaxLimit = 80f;     

    private float x = 0.0f;   
    private float y = 0.0f;   
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void Update()
    {

        if (target)
        {
            
            if (Input.GetMouseButton(1)) 
            {

                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

                
                Quaternion rotation = Quaternion.Euler(y, x, 0);

               
                target.rotation = Quaternion.Euler(0, x, 0);

               
                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
                transform.rotation = rotation;
                transform.position = position;
            }
            else if (Input.GetMouseButton(0)) 
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

                
                Quaternion rotation = Quaternion.Euler(y, x, 0);


                
                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
                transform.rotation = rotation;
                transform.position = position;
            }
            else
            {
                
                Quaternion rotation = Quaternion.Euler(y, x, 0);
                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
                transform.rotation = rotation;
                transform.position = position;
            }


            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, zoomMin, zoomMax);



        }

    }
}
