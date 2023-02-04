using UnityEngine;

public class TurnOffMeshOnCollision : MonoBehaviour
{
    [SerializeField] private string targetTag = "Untagged";
    private MeshRenderer meshRenderer;
    private Collider collider;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            Debug.Log("Collided");
            meshRenderer.enabled = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            Debug.Log("Uncollided");
            meshRenderer.enabled = true;
        }
    }
}
