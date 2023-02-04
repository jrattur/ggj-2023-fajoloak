using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSteering : MonoBehaviour
{
    [SerializeField]
    private SteeringBehaviour[] steeringBehaviours;

    // Start is called before the first frame update
    void Start()
    {
        steeringBehaviours = transform.GetComponentsInChildren<SteeringBehaviour>();
    }

    private Vector3 previousPosition;

    // Update is called once per frame
    void Update()
    {
        Vector3 steering = Vector3.zero;
        foreach (var steeringBehaviour in steeringBehaviours) {
            steering += steeringBehaviour.calculateMove();
        }

        transform.LookAt(transform.position + steering);

        previousPosition = transform.position;
        transform.position += steering * Time.deltaTime;
    }
}
