using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDraw : MonoBehaviour
{
    [SerializeField]
    private GameObject drawPrefab;

    [SerializeField]
    public float spawnTime = 0.3f;

    private float timer = 0f;

    private Vector3 startPosition = Vector3.zero, endPosition = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        endPosition = transform.position;
        timer += Time.deltaTime;

        if (timer > spawnTime) {

            Vector3 lineVector = endPosition - startPosition;

            Vector3 createPosition = Vector3.Lerp(startPosition, endPosition, 0.5f);

            var drawnObject = Instantiate(drawPrefab, createPosition, transform.rotation);
            drawnObject.transform.LookAt(startPosition);

            //drawnObject.transform.localScale = new Vector3()

            startPosition = transform.position;
            timer = 0f;
        }
    }
}
