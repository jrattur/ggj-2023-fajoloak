using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    [SerializeField]
    private float wanderSphereDistance = 0.2f;

    [SerializeField]
    private float wanderAngleLimit = 30f;

    [SerializeField]
    private float wanderCircleDistance = 5f;

    [SerializeField]
    private float wanderCircleRadius = 1f;

    [SerializeField]
    private float wanderStrength = 1f;

    [SerializeField]
    private float walkingSpeed = 1f;

    private Vector3 previousPosition, velocity;

    // Update is called once per frame
    void Update()
    {

        velocity = (transform.position - previousPosition) / Time.deltaTime;
        Vector3 wander = calculateWander(velocity) * wanderStrength;

        var steering = wander;

        previousPosition = transform.position;
        transform.position += steering * walkingSpeed * Time.deltaTime;

    }

    float wanderAngle = 0f;
    private Vector3 calculateWander(Vector3 velocity)
    {
        var wander = Vector3.zero;

        Vector3 circleCentre = transform.TransformDirection(Vector3.up * wanderSphereDistance);
        wanderAngle += UnityEngine.Random.Range(-wanderAngleLimit / 2, wanderAngleLimit / 2);
        wander = circleCentre + new Vector3(wanderCircleRadius * Mathf.Cos(wanderAngle * Mathf.Deg2Rad), Mathf.Sin(wanderAngle * Mathf.Deg2Rad));
        Debug.DrawLine(transform.position, transform.position + circleCentre, Color.blue);
        Debug.DrawLine(transform.position + circleCentre, transform.position + circleCentre + wander, Color.blue);

        return wander;
    }
}
