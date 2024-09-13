using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public Light sun;
    public float dayDuration = 120f;
    public float accelerator = 1;
    public float time = 0f;
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

        time = time + Time.deltaTime * accelerator;

        if (time >= dayDuration)
        {
            time = 0f;
        }

        float sunAngle = (time/dayDuration) * 360f;  
        sun.transform.localRotation = Quaternion.Euler(sunAngle - 90, 170, 0);

        
        if (sunAngle >= 0 && sunAngle <= 180)
        {
            
            sun.intensity = Mathf.Lerp(0, 1, (sunAngle / 180f));
        }
        else
        {
            sun.intensity = Mathf.Lerp(1, 0, ((sunAngle - 180f) / 180f));
        }

    }
}
