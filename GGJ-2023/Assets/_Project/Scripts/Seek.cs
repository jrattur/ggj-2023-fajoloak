using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    [SerializeField]
    private float seekStrength = 1f;

    [SerializeField]
    private float seekFoundDistance = 5f;

    public GameObject seekPoint;

    public bool found = false;

    public static event Action<String> OnDestroyNutrients;

    public override Vector3 calculateMove()
    {
        if (seekPoint == null) { return Vector3.zero; }
        var seek = Vector3.zero;
        seek = (seekPoint.transform.position - transform.position);

        if (seek.magnitude < seekFoundDistance) {
            found = true;
            Destroy(seekPoint);
            OnDestroyNutrients?.Invoke(transform.root.name);
        }

        return seek.normalized * seekStrength;

    }
}
