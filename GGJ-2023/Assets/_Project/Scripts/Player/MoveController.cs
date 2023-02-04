using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    float _speedByInput;
    [SerializeField]
    bool _isControlByArrow = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveVelocity = Vector3.zero;

        if (Input.GetKey(_isControlByArrow ? KeyCode.DownArrow : KeyCode.S)) {
            moveVelocity.y -= _speedByInput;
        }
        else if (Input.GetKey(_isControlByArrow ? KeyCode.UpArrow : KeyCode.W)) {
            moveVelocity.y += _speedByInput;
        }
        if (Input.GetKey(_isControlByArrow ? KeyCode.LeftArrow : KeyCode.A)) {
            moveVelocity.x -= _speedByInput;
        }
        else if (Input.GetKey(_isControlByArrow ? KeyCode.RightArrow : KeyCode.D)) {
            moveVelocity.x += _speedByInput;
        }
    }

    private void FixedUpdate()
    {
        this.transform.position += moveVelocity * Time.deltaTime;
    }

    private Vector3 moveVelocity;
}
