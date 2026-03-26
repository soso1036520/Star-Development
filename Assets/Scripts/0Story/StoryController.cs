using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    void Start()
    {
        Debug.Log("🔥 Story Start");

        // ⭐ 建立藝人（只存ID）
        ArtistData artist = new ArtistData()
        {
            artistName = "周然",
            characterID = "1女"
        };

        List<DialogueLine> story = new List<DialogueLine>();

        // ⭐ 登場
        story.Add(new DialogueLine {
            speaker = artist.characterID,
            content = "……你找我？",
            expression = "常態",
            isPlayer = false
        });

        // ⭐ 玩家開場
        story.Add(new DialogueLine {
            speaker = "玩家",
            content = "是的，我是公司新派來的經紀人，之後會負責你的所有行程安排。",
            expression = "常態",
            isPlayer = true
        });

        // ⭐ 冷回
        story.Add(new DialogueLine {
            speaker = artist.characterID,
            content = "經紀人？公司又換人了啊……動作還真快。",
            expression = "生氣",
            isPlayer = false
        });

        // ⭐ 玩家試圖解釋
        story.Add(new DialogueLine {
            speaker = "玩家",
            content = "我知道你可能對公司有些不滿，但我會盡量站在你的立場幫你爭取資源。",
            expression = "常態",
            isPlayer = true
        });

        // ⭐ 嘲諷
        story.Add(new DialogueLine {
            speaker = artist.characterID,
            content = "每個新來的人都這樣說。",
            expression = "生氣",
            isPlayer = false
        });

        // ⭐ 冷場
        story.Add(new DialogueLine {
            speaker = artist.characterID,
            content = "結果呢？還不是把我丟去接一些爛通告。",
            expression = "生氣",
            isPlayer = false
        });

        // ⭐ 玩家轉策略
        story.Add(new DialogueLine {
            speaker = "玩家",
            content = "那如果我說，我可以幫你挑掉你不想接的工作呢？",
            expression = "常態",
            isPlayer = true
        });

        // ⭐ 開始有反應
        story.Add(new DialogueLine {
            speaker = artist.characterID,
            content = "……你有這個權限？",
            expression = "常態",
            isPlayer = false
        });

        // ⭐ 玩家認真
        story.Add(new DialogueLine {
            speaker = "玩家",
            content = "沒有的話，我會想辦法爭取。",
            expression = "常態",
            isPlayer = true
        });

        // ⭐ 觀察你
        story.Add(new DialogueLine {
            speaker = artist.characterID,
            content = "你還真敢說。",
            expression = "常態",
            isPlayer = false
        });

        // ⭐ 緩和
        story.Add(new DialogueLine {
            speaker = artist.characterID,
            content = "……算了，至少比上一個好一點。",
            expression = "常態",
            isPlayer = false
        });

        // ⭐ 玩家
        story.Add(new DialogueLine {
            speaker = "玩家",
            content = "那我們可以從重新規劃你的行程開始嗎？",
            expression = "常態",
            isPlayer = true
        });

        // ⭐ 給機會
        story.Add(new DialogueLine {
            speaker = artist.characterID,
            content = "我只給你一次機會。",
            expression = "生氣",
            isPlayer = false
        });

        // ⭐ 收尾
        story.Add(new DialogueLine {
            speaker = artist.characterID,
            content = "別讓我失望。",
            expression = "常態",
            isPlayer = false
        });

        dialogueSystem.PlayDialogue(story);
    }
}