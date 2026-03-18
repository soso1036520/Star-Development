using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Mood
{
    Normal,
    Happy,
    Angry
}

public class ArtistVisual : MonoBehaviour
{
    public Image faceImage;

    public Sprite normal;
    public Sprite happy;
    public Sprite angry;
    public Sprite blink;

    private Mood currentMood = Mood.Normal;
    private bool isBlinking = false;

    void Start()
    {
        StartCoroutine(BlinkLoop());
        UpdateFace();
    }

    public void SetMood(Mood mood)
    {
        currentMood = mood;
        UpdateFace();
    }

    void UpdateFace()
    {
        if (isBlinking)
        {
            faceImage.sprite = blink;
            return;
        }

        switch (currentMood)
        {
            case Mood.Happy:
                faceImage.sprite = happy;
                break;
            case Mood.Angry:
                faceImage.sprite = angry;
                break;
            default:
                faceImage.sprite = normal;
                break;
        }
    }

    IEnumerator BlinkLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 7f));

            isBlinking = true;
            UpdateFace();

            yield return new WaitForSeconds(0.06f);

            isBlinking = false;
            UpdateFace();
        }
    }
}