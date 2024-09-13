using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    public Transform point0, point1, point2;  
    public Transform point3;  

    public int curveResolution = 50;  

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = curveResolution + 1;  
    }

    void Update()
    {
        CubicBezier();
        
    }

    void QuadraticBezier()
    {
        for (int i = 0; i <= curveResolution; i++)
        {
            float t = i / (float)curveResolution;
            Vector3 point = QuadraticBezierCalcul(t, point0.position, point1.position, point2.position);
            lineRenderer.SetPosition(i, point);
        }
    }

    void CubicBezier()
    {
        for (int i = 0; i <= curveResolution; i++)
        {
            float t = i / (float)curveResolution;
            Vector3 point = CubicBezierCalcul(t, point0.position, point1.position, point2.position, point3.position);
            lineRenderer.SetPosition(i, point);
        }
    }

    void BezierRecursive()
    {


    }

    Vector3 QuadraticBezierCalcul(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return point;
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
