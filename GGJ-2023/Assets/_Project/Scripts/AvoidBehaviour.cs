using System;
using System.Collections.Generic;
using UnityEngine;

public class AvoidBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float avoidStrength = 5f;

    [SerializeField]
    private float avoidRadius = 20f;

    public override Vector3 calculateMove()
    {
        RaycastHit hit;

        float distanceToObstacle = 0;

        Vector3 hitVector = Vector3.zero;

        var zzz = Physics.SphereCastAll(transform.position, avoidRadius, Vector3.forward, LayerMask.NameToLayer("Avoidable"));

        foreach (var qqq in zzz) {
            Debug.Log("zzz" + qqq);
            hitVector = (qqq.point - transform.position).normalized;
        }

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        //if (Physics.SphereCast(transform.position, avoidRadius, Vector3.forward, out hit))
        //{
        //    Debug.Log("HIT!!!");
        //    distanceToObstacle = hit.distance;

        //     hitVector = (hit.point - transform.position).normalized;
        //    Debug.Log(hitVector);
        //}

        return -hitVector * avoidStrength;

    }
}
