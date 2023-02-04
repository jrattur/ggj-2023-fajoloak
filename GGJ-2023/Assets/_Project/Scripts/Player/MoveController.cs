using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    float _speedByInput;
    [SerializeField]
    float _rotationSpeedDeg;
    [SerializeField]
    bool _isControlByArrow = false;

    // Start is called before the first frame update
    void Start()
    {
        _position = this.transform.position;
        _rotation = this.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        _moveVelocity = Vector3.zero;

        if (Input.GetKey(_isControlByArrow ? KeyCode.DownArrow : KeyCode.S)) {
            _moveVelocity.y -= _speedByInput;
        }
        else if (Input.GetKey(_isControlByArrow ? KeyCode.UpArrow : KeyCode.W)) {
            _moveVelocity.y += _speedByInput;
        }
        if (Input.GetKey(_isControlByArrow ? KeyCode.LeftArrow : KeyCode.A)) {
            _moveVelocity.x -= _speedByInput;
        }
        else if (Input.GetKey(_isControlByArrow ? KeyCode.RightArrow : KeyCode.D)) {
            _moveVelocity.x += _speedByInput;
        }

    }

    private void FixedUpdate()
    {
        _position += _moveVelocity * Time.deltaTime;
        float angle = _rotation.eulerAngles.z;
        if (180f < angle) {
            angle -= 360f;
        }
        goalAngle = Mathf.Atan2(_moveVelocity.y, Mathf.Abs(_moveVelocity.x)) * Mathf.Rad2Deg;
        float rotateY = _moveVelocity.x < 0f ? 180f : 0f;
        // if (90f < Mathf.Abs(goalAngle)) {
        //     goalAngle = (180f - Mathf.Abs(goalAngle)) * Mathf.Sign(goalAngle);
        //     rotateY = 180f;
        // }
        goalAngle = Mathf.Clamp(goalAngle, -45f, 45f);

        float diffToGoal = goalAngle - angle;
        Debug.Log($"{diffToGoal}, {goalAngle}, {angle}");
        if (Mathf.Abs(diffToGoal) < _rotationSpeedDeg) {
            angle = goalAngle;
        } else {
            angle += Mathf.Sign(diffToGoal) * _rotationSpeedDeg;
        }
        _rotation = Quaternion.Euler(0f, rotateY, angle);
        this.transform.SetPositionAndRotation(_position, _rotation);
    }

    private Vector3 _moveVelocity;
    private Vector3 _position;
    private Quaternion _rotation;
    [SerializeField]
    private float goalAngle;
}
