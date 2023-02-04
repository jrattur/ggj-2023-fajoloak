using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCamera : MonoBehaviour
{
    public Transform target; // the object to rotate around
    public float speed = 10.0f; // rotation speed
    public Camera cam;
    public float defaultFov = 90;

    private Vector2 _rightStick;
    private bool rotateRight;
    private bool rotateLeft;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotateRight = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rotateRight = false;

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotateLeft = true;

        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rotateLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
         cam.fieldOfView = 30;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
         cam.fieldOfView = 60;
        }


        if (rotateRight)
        {
            transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
        }
        if (rotateLeft)
        {
            transform.RotateAround(target.position, Vector3.down, speed * Time.deltaTime);
        }

        if (_rightStick.x != 0f) {
            transform.RotateAround(target.position, Vector3.up, speed * _rightStick.x * Time.deltaTime);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _rightStick = context.ReadValue<Vector2>();
    }


    
}