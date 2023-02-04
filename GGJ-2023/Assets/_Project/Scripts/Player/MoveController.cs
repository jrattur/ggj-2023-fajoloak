using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    float _speedMove;
    [SerializeField]
    float _rotationSpeedDeg;
    [SerializeField]
    float _brakeUpDown;
    [SerializeField]
    float _accelUpDown;
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
        if (0f < _moveVelocity.y) {
            Debug.Log($"{_moveVelocity.y} + {_accelUpDown * _upDownSign} - {_brakeUpDown}");
            _moveVelocity.y = Mathf.Clamp(_moveVelocity.y + _accelUpDown * _upDownSign - _brakeUpDown, 0f, 1f);
        }
        else if (_moveVelocity.y < 0f) {
            Debug.Log($"{_moveVelocity.y} + {_accelUpDown * _upDownSign} - {_brakeUpDown}");
            _moveVelocity.y = Mathf.Clamp(_moveVelocity.y + _accelUpDown * _upDownSign + _brakeUpDown, -1f, 0f);
        } else {
            Debug.Log($"{_moveVelocity.y} + {_accelUpDown * _upDownSign}");
            _moveVelocity.y = Mathf.Clamp(_moveVelocity.y + _accelUpDown * _upDownSign, -1f, 1f);
        }
    }

    public void OnMoveXZ(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        _moveVelocity.x = move.x;
        _moveVelocity.z = move.y;
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _upDownSign = 1f;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _upDownSign = 0f;
        }

    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _upDownSign = -1f;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _upDownSign = 0f;
        }
    }

    private void FixedUpdate()
    {
        _position += _moveVelocity * _speedMove * Time.deltaTime;
        Vector3 direction = _moveVelocity;
        direction.Normalize();
        if (direction != Vector3.zero) {
            _rotation = Quaternion.RotateTowards(Quaternion.FromToRotation(Vector3.right, direction), _rotation, _rotationSpeedDeg);
        }
        Debug.Log(_position);

        // float angle = _rotation.eulerAngles.z;
        // if (180f < angle) {
        //     angle -= 360f;
        // }
        // goalAngle = Mathf.Atan2(_moveVelocity.y, Mathf.Abs(_moveVelocity.x)) * Mathf.Rad2Deg;
        // float rotateY = _moveVelocity.x < 0f ? 180f : 0f;
        // // if (90f < Mathf.Abs(goalAngle)) {
        // //     goalAngle = (180f - Mathf.Abs(goalAngle)) * Mathf.Sign(goalAngle);
        // //     rotateY = 180f;
        // // }
        // goalAngle = Mathf.Clamp(goalAngle, -45f, 45f);

        // float diffToGoal = goalAngle - angle;
        // if (Mathf.Abs(diffToGoal) < _rotationSpeedDeg) {
        //     angle = goalAngle;
        // } else {
        //     angle += Mathf.Sign(diffToGoal) * _rotationSpeedDeg;
        // }
        this.transform.SetPositionAndRotation(_position, _rotation);
    }

    private Vector3 _moveVelocity;
    private Vector3 _position;
    private Quaternion _rotation;
    private float _upDownSign;
    [SerializeField]
    private float goalAngle;
}
