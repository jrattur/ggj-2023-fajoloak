using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject rootPrefab;

    [SerializeField]
    private float rootMultiplierTimer = 5f;

    [SerializeField]
    private float rootMultiplierUpdateCounter = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        rootMultiplierUpdateCounter += Time.deltaTime;

        if (rootMultiplierUpdateCounter > rootMultiplierTimer)
        {
            Instantiate(rootPrefab);
            rootMultiplierUpdateCounter = 0f;
        }


    }
}
