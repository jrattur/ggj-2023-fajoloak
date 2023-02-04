using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : SteeringBehaviour
{

    [SerializeField]
    private float wanderSphereDistance = 0.2f;

    [SerializeField]
    private float wanderAngleLimit = 30f;

    [SerializeField]
    private float wanderCircleRadius = 1f;

    [SerializeField]
    private float wanderSpeed = 1f;

    float wanderAngle = 0f;

    public override Vector3 calculateMove()
    {
        var wander = Vector3.zero;

        Vector3 circleCentre = transform.TransformDirection(Vector3.up * wanderSphereDistance);
        wanderAngle += UnityEngine.Random.Range(-wanderAngleLimit / 2, wanderAngleLimit / 2);
        wander = circleCentre + new Vector3(wanderCircleRadius * Mathf.Cos(wanderAngle * Mathf.Deg2Rad), Mathf.Sin(wanderAngle * Mathf.Deg2Rad));
        Debug.DrawLine(transform.position, transform.position + circleCentre, Color.blue);
        Debug.DrawLine(transform.position + circleCentre, transform.position + circleCentre + wander, Color.blue);

        return wander * wanderSpeed;
    }
}
