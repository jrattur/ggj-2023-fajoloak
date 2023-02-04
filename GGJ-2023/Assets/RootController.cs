using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RootController : MonoBehaviour
{

    [SerializeField]
    private float nutrients = 100f;

    [SerializeField]
    private int numberOfRoots = 10;

    [SerializeField]
    private GameObject rootPrefab;

    [SerializeField]
    private Seek[] seekComponents;

    [SerializeField]
    private GameObject closestNutrient = null;

    Stopwatch stopWatch;
    bool stopped = false;

    private void HandleEvent(string parentName)
    {
        //if(gameObject != null) { 
        //if (parentName == gameObject.transform.root.name) {
        //    stopWatch.Reset();
        //    stopWatch.Start();
        //    stopped = true;
        //    foreach (Transform child in transform) {
        //        child.gameObject.SetActive(false);
        //    }
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        stopWatch = new Stopwatch();

        Seek.OnDestroyNutrients += HandleEvent;
        spawnRoots(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped) {
            if (stopWatch.Elapsed.Seconds > 5f) {
                stopWatch.Stop();
                stopWatch.Reset();
                stopped = false;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }

            }
        }


        if (closestNutrient == null) { updateClosestNutrient(); }

        foreach (var seek in seekComponents) {
            if (seek.found) {
                updateClosestNutrient();
            }
        }
    }

    private int timesUpdateClosestFoundCalled = 0;
    private void updateClosestNutrient() {

        timesUpdateClosestFoundCalled++;

        foreach (var nutrient in GameObject.FindGameObjectsWithTag("Nutrient"))
        {
            if (closestNutrient == null)
            {
                closestNutrient = nutrient;
               
            }
            else if ((transform.position.sqrMagnitude - nutrient.transform.position.sqrMagnitude) > (transform.position.sqrMagnitude - closestNutrient.transform.position.sqrMagnitude))
            {
                closestNutrient = nutrient;
            }
        }

        foreach (var seek in transform.GetComponentsInChildren<Seek>()) { seek.seekPoint = closestNutrient; }

    }

    private void spawnRoots(Vector3 position) {
        for (int i = 0; i < numberOfRoots; i++)
        {
            GameObject instantiatedObject = Instantiate(rootPrefab, position, transform.rotation, transform);
        }
        seekComponents = transform.GetComponentsInChildren<Seek>();

        updateClosestNutrient();
    }
}
