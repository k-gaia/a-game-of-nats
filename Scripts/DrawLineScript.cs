using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineScript : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private float counter;
    private float distance;

    public Transform origin;
    public Transform destination;

    public float drawSpeed = 6f;


    // Start is called before the first frame update
    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);

        distance = Vector3.Distance(origin.position, destination.position);



    }

    // Update is called once per frame
    void Update()
    {
        
        if (counter < distance)
        {

            counter += .1f / drawSpeed;

            //
            float x = Mathf.Lerp(0, distance, counter);

            // Both Airport Positions
            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;


            // Unit Vector in direction of (A -> B), Multiply by length + starting point (Animation of Line Draw)
            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            lineRenderer.SetPosition(1, pointAlongLine);

        }




    }
}
