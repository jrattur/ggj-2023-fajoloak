using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    GameObject _gemPrefab;
    [SerializeField]
    int _maxNum;
    [SerializeField]
    int _spawnInterval;
    [SerializeField]
    int _initialSpawnWait;
    [SerializeField]
    Vector3 _spawnAreaMin;
    [SerializeField]
    Vector3 _spawnAreaMax;


    // Start is called before the first frame update
    void Start()
    {
        _timer = _initialSpawnWait;
        if (_gemPrefab != null) {
            _gemCollisionRadius = _gemPrefab.GetComponent<SphereCollider>().radius;
        } else {
            _gemCollisionRadius = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver()) { return; }

            if (0f < _timer)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    _timer = _spawnInterval;

                    Spawn();
                }
            }
    }

    private static bool IsGameOver()
    {
        return Time.timeScale < 1f;
    }

    private void Spawn()
    {
        Vector3 randomPosition;
        do {
            randomPosition = new Vector3(
                UnityEngine.Random.Range(_spawnAreaMin.x, _spawnAreaMax.x),
                UnityEngine.Random.Range(_spawnAreaMin.y, _spawnAreaMax.y),
                UnityEngine.Random.Range(_spawnAreaMin.z, _spawnAreaMax.z));
        } while (!Physics.CheckSphere(randomPosition, _gemCollisionRadius));

        GameObject.Instantiate(_gemPrefab, randomPosition, Quaternion.identity);
    }

    float _timer;
    private float _gemCollisionRadius;
}
