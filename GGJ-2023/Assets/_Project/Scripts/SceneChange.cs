using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneName;

    private InputAction _pressAnyKeyAction =
                    new InputAction(type: InputActionType.PassThrough, binding: "*/<Button>", interactions: "Press");

    private void OnEnable() => _pressAnyKeyAction.Enable();
    private void OnDisable() => _pressAnyKeyAction.Disable();

    private void Update()
    {
        if (Input.anyKey || _pressAnyKeyAction.triggered)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
