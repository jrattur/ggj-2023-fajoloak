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

    [SerializeField]
    private float rootMultiplierTimer = 5f;

    [SerializeField]
    private float rootMultiplierUpdateCounter = 0f;

    [SerializeField]
    private float rootMultiplierAmount = 1.1f;


    Stopwatch stopWatch;
    bool stopped = false;

    private GameObject[] children;

    private void HandleEvent(string parentName)
    {

        if (this != null && gameObject != null)
        {
            UnityEngine.Debug.Log("zzz" + parentName + "ccc" + gameObject.transform.root.name);
            if (parentName == gameObject.transform.root.name)
            {
                Seek.OnDestroyNutrients -= HandleEvent;
                UnityEngine.Debug.Log("sadfsdafds");
                stopWatch.Reset();
                stopWatch.Start();
                stopped = true;

                foreach (Transform child in GetComponentsInChildren<Transform>(true))
                {
                    if (child.name != gameObject.name) { 
                    child.gameObject.SetActive(false);
                }
                }
            }
        }
    }

    private void onCollectGemHandler() {

        if (this != null)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in transform)
            {
                if (child.tag == "Root")
                {
                    Instantiate(child.gameObject, child.transform.position, transform.rotation);
                }
            }

            children.RemoveRange(0, children.Count / 2);

            foreach (var child in children)
            {
                Destroy(child);
            }

            foreach (var agentSteering in transform.GetComponentsInChildren<AgentSteering>())
            {
                agentSteering.rootSpeed *= -rootMultiplierAmount;
                agentSteering.transform.GetComponentInChildren<PathDraw>().spawnTime *= -(1 / rootMultiplierAmount);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        stopWatch = new Stopwatch();

        Seek.OnDestroyNutrients += HandleEvent;
        MoveController.OnCollectGem += onCollectGemHandler;
        spawnRoots(transform.position);
    }



    // Update is called once per frame
    void Update()
    {
        if (stopped) {
            if (stopWatch.Elapsed.Seconds > 5f) {
                Seek.OnDestroyNutrients += HandleEvent;
                UnityEngine.Debug.Log("Renable!!!!");
                stopWatch.Stop();
                stopWatch.Reset();
                stopped = false;
                foreach (Transform child in GetComponentsInChildren<Transform>(true))
                {
                    child.gameObject.SetActive(true);
                }

            }
        }


        rootMultiplierUpdateCounter += Time.deltaTime;

        if (rootMultiplierUpdateCounter > rootMultiplierTimer) {

            foreach (var agentSteering in transform.GetComponentsInChildren<AgentSteering>()) {
                agentSteering.rootSpeed *= rootMultiplierAmount;
                agentSteering.transform.GetComponentInChildren<PathDraw>().spawnTime *= (1 / rootMultiplierAmount);
            }

            foreach (Transform child in transform)
            {
                if (child.tag == "Root")
                {
                    Instantiate(child.gameObject, child.transform.position, transform.rotation);
                }
            }

            rootMultiplierUpdateCounter = 0f;
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

        List<GameObject> nutrients = new List<GameObject>(GameObject.FindGameObjectsWithTag("Nutrient"));

        var rootsGroups = new List<GameObject>(GameObject.FindGameObjectsWithTag("RootsGroup"));

        var rootsGroupsCloestObjects = new List<GameObject>();

        foreach(var rootsGroup in rootsGroups){
            nutrients.Remove(rootsGroup.GetComponent<RootController>().closestNutrient);
        }

        foreach (var nutrient in nutrients)
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
