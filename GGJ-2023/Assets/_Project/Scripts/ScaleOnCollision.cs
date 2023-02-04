using UnityEngine;

public class ScaleOnCollision : MonoBehaviour
{
    public float scaleFactor = 2.0f;
    public string targetTag = "Target";
    public GameObject target;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            transform.localScale *= scaleFactor;
            target.transform.position=new Vector3(transform.position.x,transform.position.y + 100,transform.position.z);

        }
    }
}