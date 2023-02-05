using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_started) { return; }

        if (0f < _slowTime) {
            _slowTime -= Time.unscaledDeltaTime;
            _shakeTime -= Time.unscaledDeltaTime;
            if (_slowTime <= 0f) {
                Time.timeScale = 0f;
                // Camera.main.transform.position = _cameraOriginalPos;
            } else if (0f < _shakeTime) {
                if (_shakeTime % 0.05f < 0.01f) {
                    Vector3 shakeOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    Camera.main.transform.position = _cameraOriginalPos + shakeOffset;
                }
            }
        } else if (0f < _stopTime) {
            _stopTime -= Time.unscaledDeltaTime;
            if (_stopTime <= 0f) {
                Time.timeScale = 1f;

                SceneManager.LoadScene (SceneManager.GetActiveScene().name);
                // SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void CaughtByRoots() 
    {
        if (_started) { return; }
        _started = true;
        Time.timeScale = _slowScale;
        UnityEngine.InputSystem.InputSystem.DisableAllEnabledActions();

        _cameraOriginalPos = Camera.main.gameObject.transform.position;
        // stop player physics
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody != null) {
            rigidbody.isKinematic = true;
        }
    }

    bool _started;
    [SerializeField]
    float _slowTime = 1.0f;
    [SerializeField]
    float _slowScale = 0.5f;
    [SerializeField]
    float _shakeTime = 0.2f;
    [SerializeField]
    float _stopTime = 0.5f;
    Vector3 _cameraOriginalPos;
    Vector3 _cameraShakeOffset;
}
