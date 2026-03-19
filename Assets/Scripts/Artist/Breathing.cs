using UnityEngine;
//呼吸
public class Breathing : MonoBehaviour
{
    public float speed = 1.5f;
    public float scaleAmount = 0.008f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * speed) * scaleAmount;
        transform.localScale = originalScale * scale;
    }
}