using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    float _speedByInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.DownArrow)) {
            moveVelocity.y -= _speedByInput;
        }
        else if (Input.GetKey(KeyCode.UpArrow)) {
            moveVelocity.y += _speedByInput;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveVelocity.x -= _speedByInput;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            moveVelocity.x += _speedByInput;
        }
    }

    private void FixedUpdate()
    {
        this.transform.position += moveVelocity * Time.deltaTime;
    }

    private Vector3 moveVelocity;
}
