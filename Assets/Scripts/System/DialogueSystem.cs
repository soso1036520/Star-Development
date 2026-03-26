using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string content;

    public string expression; // "生氣" / "開心"
    public bool isPlayer;
}

public class DialogueSystem : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    [Header("角色UI")]
    public Image mainCharacter;   // NPC（左）
    public Image playerCharacter; // 玩家（右）

    public float typingSpeed = 0.05f;

    private List<DialogueLine> dialogue;
    private int currentIndex = 0;

    private Coroutine typingCoroutine;
    private Coroutine fadeCoroutine;

    private bool isTyping = false;
    private bool isPlaying = false;

    private int mid = 0;
    public ArtistBlinkController blinkController;

    void Start()
    {
        // ⭐ 一開始先隱藏（用透明度，不用SetActive）
        SetCharacterVisible(mainCharacter, false);
        SetCharacterVisible(playerCharacter, false);
    }

    void Update()
    {
        if (!isPlaying) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogue[currentIndex].content;
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    public void PlayDialogue(List<DialogueLine> newDialogue)
    {
        dialogue = newDialogue;
        currentIndex = 0;
        isPlaying = true;

        ShowLine();
    }

    void ShowLine()
    {
        if (dialogue == null || currentIndex >= dialogue.Count)
        {
            EndDialogue();
            return;
        }

        var line = dialogue[currentIndex];

        // ⭐ 名字
        nameText.text = line.speaker;

        // ⭐ 取得立繪
        Sprite sprite = GetSprite(line);
        if (sprite == null)
        {
            Debug.LogError("❌ Sprite 為 null，跳過這行");
            NextLine();
            return;
        }

        if (line.isPlayer)
        {
            // 👉 玩家顯示
            playerCharacter.sprite = sprite;

            SetCharacterVisible(playerCharacter, true);
            PlayFade(playerCharacter);
        }
        else
        {
            // 👉 NPC顯示
            mainCharacter.sprite = sprite;

            //SetCharacterVisible(mainCharacter, true);
            SetCharacterVisible(playerCharacter, false);
            Debug.Log("使用NPCID：" + line.speaker);
            if(mid==0){
            PlayFade(mainCharacter);
            if(blinkController != null)
            {
                Debug.Log("啟動眨眼" + line.speaker);
                blinkController.StartBlink(line.speaker);
            }
            mid=1;
            }
        }

        // ⭐ 打字機
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(line.content));
    }

    // ⭐ 統一抓圖
    Sprite GetSprite(DialogueLine line)
    {
        if (line.isPlayer)
        {
            var player = PlayerCompany.Instance.player;
             if (player == null){
                player = new PlayerData()
                {
                    playerID = "1p",
                    playerName = "玩家"
                };
                PlayerCompany.Instance.player = player;
            }
            Debug.Log("使用玩家ID：" + player.playerID);
            return PortraitManager.GetPlayerPortrait(player.playerID, line.expression);
        }else
        {
            return PortraitManager.GetCharacterPortrait(line.speaker, line.expression);
        }
        
    }

    IEnumerator TypeText(string content)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in content)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // ⭐ 淡入（安全版）
    void PlayFade(Image img)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeIn(img, 0.2f));
    }

    IEnumerator FadeIn(Image img, float duration)
    {
        if (img == null) yield break;

        Color c = img.color;
        c.a = 0;
        img.color = c;

        float t = 0;

        while (t < duration)
        {
            if (img == null) yield break;

            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / duration);
            img.color = c;
            yield return null;
        }

        c.a = 1;
        img.color = c;
    }

    // ⭐ 不用 SetActive！改用透明度
    void SetCharacterVisible(Image img, bool visible)
    {
        if (img == null) return;

        Color c = img.color;
        c.a = visible ? 1f : 0f;
        img.color = c;
    }

    public void NextLine()
    {
        currentIndex++;
        ShowLine();
    }

    void EndDialogue()
    {
        isPlaying = false;
        Debug.Log("對話結束");
    }
}