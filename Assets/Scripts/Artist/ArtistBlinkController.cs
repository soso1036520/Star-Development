using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//動畫打開眼睛閉上眼睛
public class ArtistBlinkController : MonoBehaviour
{
    public Image portraitImage;

    private string characterID;
    private Coroutine blinkCoroutine;

    // ⭐ 開始眨眼
    public void StartBlink(string id)
    {
        characterID = id;

        // 停掉舊的
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        blinkCoroutine = StartCoroutine(BlinkLoop());
    }

    // ⭐ 停止眨眼
    public void StopBlink()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
    }

    IEnumerator BlinkLoop()
    {
        while (true)
        {
            // ⭐ 常態（睜眼）
            SetExpression("常態");

            yield return new WaitForSeconds(Random.Range(2f, 4f));

            // ⭐ 閉眼
            SetExpression("閉眼");

            yield return new WaitForSeconds(0.15f);

            // ⭐ 回常態
            SetExpression("常態");

            yield return new WaitForSeconds(0.1f);
        }
    }

    void SetExpression(string expression)
    {
        if (portraitImage == null)
        {
            Debug.LogError("❌ portraitImage 是 null！");
            return;
        }

        if (PortraitDatabase.Instance == null)
        {
            Debug.LogError("❌ PortraitDatabase.Instance 是 null！");
            return;
        }

        if (string.IsNullOrEmpty(characterID))
        {
            Debug.LogError("❌ characterID 是空的！");
            return;
        }

        Sprite sprite = PortraitDatabase.Instance
            .GetPortrait(characterID, expression);

        if (sprite == null)
        {
            Debug.LogWarning($"❌ 找不到圖片：{characterID}_{expression}");
            return;
        }

        portraitImage.sprite = sprite;
    }
}