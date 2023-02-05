using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    GemInfo _gemInfo;
    [SerializeField]
    private AudioSource _gemGetSound;
    [SerializeField]
    float _speedMove;
    [SerializeField]
    float _rotationSpeedDeg;
    [SerializeField]
    float _brakeUpDown;
    [SerializeField]
    float _accelUpDown;

    public static event Action OnCollectGem;

    // Start is called before the first frame update
    void Start()
    {
        if (_gemInfo == null) {
            _gemInfo = FindFirstObjectByType<GemInfo>();
        }
        _body = this.GetComponent<Rigidbody>();
        _position = this.transform.position;
        _rotation = this.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (0f < _inputVelocity.y) {
            _inputVelocity.y = Mathf.Clamp(_inputVelocity.y + _accelUpDown * _upDownSign - _brakeUpDown, 0f, 1f);
        }
        else if (_inputVelocity.y < 0f) {
            _inputVelocity.y = Mathf.Clamp(_inputVelocity.y + _accelUpDown * _upDownSign + _brakeUpDown, -1f, 0f);
        } else {
            _inputVelocity.y = Mathf.Clamp(_inputVelocity.y + _accelUpDown * _upDownSign, -1f, 1f);
        }
    }

    public void OnMoveXZ(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        _inputVelocity.x = move.x;
        _inputVelocity.z = move.y;
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
        Vector3 cameraForwardXZ = Camera.main.transform.forward;
        cameraForwardXZ.y = 0f;
        cameraForwardXZ.Normalize();
        Vector3 cameraRightXZ = Camera.main.transform.right;
        cameraRightXZ.y = 0f;
        cameraRightXZ.Normalize();
        Vector3 moveVelocity = _inputVelocity.z * cameraForwardXZ
            + _inputVelocity.x * cameraRightXZ
            + _inputVelocity.y * Vector3.up;
        moveVelocity *= _speedMove;
        Vector3 direction = moveVelocity;
        direction.Normalize();
        if (direction != Vector3.zero) {
            _rotation = Quaternion.RotateTowards(Quaternion.FromToRotation(Vector3.forward, direction), _rotation, _rotationSpeedDeg);
        }

        if (_body == null) {
            _position += moveVelocity * Time.deltaTime;
            this.transform.SetPositionAndRotation(_position, _rotation);
        } else {
            if (_body.velocity.magnitude < _speedMove) {
                _body.AddForce(moveVelocity);
            }

            _body.rotation = _rotation;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Gem") {
            other.gameObject.SetActive(false);
            _gemInfo.AddGemNum();
            OnCollectGem.Invoke();
            _gemGetSound.Play();
        }
    }

    private Rigidbody _body;
    private Vector3 _inputVelocity;
    private Vector3 _position;
    private Quaternion _rotation;
    private float _upDownSign;
}
