using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string content;
    public Mood mood;
}

public class DialogueSystem : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public ArtistVisual artistVisual;

    public float typingSpeed = 0.1f;

    private List<DialogueLine> dialogue = new List<DialogueLine>();
    private int currentIndex = 0;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    void Start()
    {
        CreateTestDialogue();
        ShowLine();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 👉 如果還在打字 → 直接顯示全文
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

    void ShowLine()
    {
        if (currentIndex >= dialogue.Count) return;

        var line = dialogue[currentIndex];

        nameText.text = line.speaker;

        // 🔥 表情切換
        artistVisual.SetMood(line.mood);

        // 🔥 啟動打字機
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(line.content));
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

    public void NextLine()
    {
        currentIndex++;

        if (currentIndex < dialogue.Count)
        {
            ShowLine();
        }
        else
        {
            Debug.Log("對話結束");
        }
    }

    // 👉 劇情資料
    void CreateTestDialogue()
    {
        dialogue.Add(new DialogueLine {
            speaker = "玩家",
            content = "感謝你加入我們公司，希望我們能創造雙贏！",
            mood = Mood.Normal
        });

        dialogue.Add(new DialogueLine {
            speaker = "周燃",
            content = "合作愉快。",
            mood = Mood.Normal
        });

        dialogue.Add(new DialogueLine {
            speaker = "玩家",
            content = "我們的第一個藝人是顧言澤，一位冷系頂流練習生。",
            mood = Mood.Normal
        });

        dialogue.Add(new DialogueLine {
            speaker = "周燃",
            content = "顧言澤？聽說他之前得罪了公司高層，被雪藏了。",
            mood = Mood.Angry
        });

        dialogue.Add(new DialogueLine {
            speaker = "玩家",
            content = "對，但他有很高的顏值和舞蹈能力，希望你們共識愉快！",
            mood = Mood.Normal
        });

        dialogue.Add(new DialogueLine {
            speaker = "周燃",
            content = "我不想管任何人，也不想跟任何人組團，希望你能尊重我的想法。",
            mood = Mood.Angry
        });

        dialogue.Add(new DialogueLine {
            speaker = "玩家",
            content = "這樣啊...好的，我會盡量讓你專注於個人發展。",
            mood = Mood.Normal
        });

        dialogue.Add(new DialogueLine {
            speaker = "周燃",
            content = "那就請多指教了。",
            mood = Mood.Happy
        });
    }
}