using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    [SerializeField]
    private float seekStrength = 1f;

    public GameObject seekPoint;

    public override Vector3 calculateMove()
    {

        var seek = Vector3.zero;

        seek = (seekPoint.transform.position - transform.position).normalized * seekStrength;

        return seek;

    }
}
