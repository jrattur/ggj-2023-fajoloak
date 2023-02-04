using System;
using System.Collections.Generic;
using UnityEngine;

public class AvoidBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float avoidRayLength = 5f;

    [SerializeField]
    private float avoidRayAngle = 20f;

    [SerializeField]
    private float avoidStrength = 10f;

    public override Vector3 calculateMove()
    {
        var avoid = Vector3.zero;

        List<Tuple<Vector3, Vector3>> avoidRays = new List<Tuple<Vector3, Vector3>>();

        avoidRays.Add(new Tuple<Vector3, Vector3>(
            transform.TransformDirection(new Vector3(avoidRayLength * Mathf.Cos((90 + avoidRayAngle) * Mathf.Deg2Rad), avoidRayLength * Mathf.Sin((90 + avoidRayAngle) * Mathf.Deg2Rad))),
            transform.TransformDirection(new Vector3(-(avoidRayLength * Mathf.Cos((90 + avoidRayAngle) * Mathf.Deg2Rad)), avoidRayLength * Mathf.Sin((90 + avoidRayAngle) * Mathf.Deg2Rad)))));
        avoidRays.Add(new Tuple<Vector3, Vector3>(
            transform.TransformDirection(new Vector3(-(avoidRayLength * Mathf.Cos((90 + avoidRayAngle) * Mathf.Deg2Rad)), avoidRayLength * Mathf.Sin((90 + avoidRayAngle) * Mathf.Deg2Rad))),
            transform.TransformDirection(new Vector3(avoidRayLength * Mathf.Cos((90 + avoidRayAngle) * Mathf.Deg2Rad), avoidRayLength * Mathf.Sin((90 + avoidRayAngle) * Mathf.Deg2Rad)))));
        avoidRays.Add(new Tuple<Vector3, Vector3>(
            transform.TransformDirection(new Vector3(avoidRayLength * Mathf.Cos((90 + avoidRayAngle * 2) * Mathf.Deg2Rad), avoidRayLength * Mathf.Sin((90 + avoidRayAngle * 2) * Mathf.Deg2Rad))),
            transform.TransformDirection(new Vector3(-avoidRayLength * Mathf.Cos((90 + avoidRayAngle * 2) * Mathf.Deg2Rad), avoidRayLength * Mathf.Sin((90 + avoidRayAngle * 2) * Mathf.Deg2Rad)))));
        avoidRays.Add(new Tuple<Vector3, Vector3>(
            transform.TransformDirection(new Vector3(-(avoidRayLength * Mathf.Cos((90 + avoidRayAngle * 2) * Mathf.Deg2Rad)), avoidRayLength * Mathf.Sin((90 + avoidRayAngle * 2) * Mathf.Deg2Rad))),
            transform.TransformDirection(new Vector3(avoidRayLength * Mathf.Cos((90 + avoidRayAngle * 2) * Mathf.Deg2Rad), avoidRayLength * Mathf.Sin((90 + avoidRayAngle * 2) * Mathf.Deg2Rad)))));

        foreach (Tuple<Vector3, Vector3> avoidRayTuple in avoidRays) { Debug.DrawLine(transform.position, transform.position + avoidRayTuple.Item1, Color.red); }

        foreach (Tuple<Vector3, Vector3> avoidRayTuple in avoidRays)
        {
            var avoidRay = avoidRayTuple.Item1;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, avoidRay, avoidRay.magnitude, LayerMask.GetMask("Avoidable"));
            if (hit.collider != null)
            {
                return avoidRayTuple.Item2.normalized;
            }
        }
        return avoid.normalized * avoidStrength;
    }
}
