using UnityEngine;
//玩家藝人
public static class PortraitManager
{
    // ⭐ 玩家立繪
    public static Sprite GetPlayerPortrait(string playerID, string expression)
    {
        Debug.Log("找玩家圖片：" + playerID);
        string path = $"Character illustration/Player/{playerID}_{expression}";
        Sprite sprite = Resources.Load<Sprite>(path);

        if (sprite == null)
        {
            Debug.LogError("❌ 找不到玩家圖片：" + path);
        }

        return sprite;
    }

    // ⭐ 藝人 / NPC 立繪
    public static Sprite GetCharacterPortrait(string characterID, string expression)
    {
        string genderFolder = GetGenderFolder(characterID);

        string path = $"Character illustration/{genderFolder}/{characterID}_{expression}";
        Sprite sprite = Resources.Load<Sprite>(path);

        if (sprite == null)
        {
            Debug.LogError("❌ 找不到角色圖片：" + path);
        }

        return sprite;
    }

    static string GetGenderFolder(string characterID)
    {
        if (characterID.EndsWith("女"))
            return "Female";
        else if (characterID.EndsWith("男"))
            return "Male";

        Debug.LogError("❌ 無法判斷性別：" + characterID);
        return "Female"; // 預設避免炸
    }
}