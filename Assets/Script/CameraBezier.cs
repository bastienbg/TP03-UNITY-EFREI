using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBezierPath : MonoBehaviour
{
    public Transform point0, point1, point2, point3;  
    public float speed = 5f;  

    private float t = 0.0f;  

    void Update()
    {
        if (t <= 1.0f)
        {
           
            Vector3 newPosition = CubicBezierCalcul(t, point0.position, point1.position, point2.position, point3.position);

            
            transform.position = newPosition;

            
            Vector3 forward = CubicBezierCalcul(t + 0.01f, point0.position, point1.position, point2.position, point3.position) - newPosition;
            transform.forward = forward;

            
            t += Time.deltaTime * speed;
        }
    }

    Vector3 CubicBezierCalcul(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = (uuu * p0) + (3 * uu * t * p1) + (3 * u * tt * p2) + (ttt * p3);
        return point;
    }
}

