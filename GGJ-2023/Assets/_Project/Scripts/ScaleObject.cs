using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    public float scaleSpeed = 1.0f;

    void Update()
    {
        float scale = 0.7f + Mathf.PingPong(Time.time * scaleSpeed, 0.8f);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
