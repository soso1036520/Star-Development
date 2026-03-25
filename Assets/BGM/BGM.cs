using System.Collections;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 2f;

    void Start()
    {
        audioSource.volume = 0f;
        audioSource.Play();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, 0.5f, t / fadeDuration);
            yield return null;
        }
    }
}