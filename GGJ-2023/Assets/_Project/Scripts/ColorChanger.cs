using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    public float changeSpeed = 1.0f;

    private Renderer renderer;
    private float t;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = startColor;
    }

    void Update()
    {
        t += changeSpeed * Time.deltaTime;
        renderer.material.color = Color.Lerp(startColor, endColor, t);
    }
}
