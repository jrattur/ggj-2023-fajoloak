using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    [SerializeField]
    public float seekStrength = 1f;

    [SerializeField]
    public float seekFoundDistance = 5f;

    public GameObject seekPoint;

    public bool found = false;

    public static event Action<String> OnDestroyNutrients;

    public override Vector3 calculateMove()
    {
        if (seekPoint == null) { return Vector3.zero; }
        var seek = Vector3.zero;
        seek = (seekPoint.transform.position - transform.position);

        if (seek.magnitude < seekFoundDistance) {
            var gameOverCtrl = seekPoint.GetComponent<GameOverController>();
            if (gameOverCtrl) {
                gameOverCtrl.CaughtByRoots();
            } else {
                found = true;
                OnDestroyNutrients?.Invoke(transform.root.name);
                Destroy(seekPoint.gameObject);
            }
        }

        return seek.normalized * seekStrength;

    }
}
