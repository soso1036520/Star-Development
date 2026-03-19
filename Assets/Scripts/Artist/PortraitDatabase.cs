using UnityEngine;
using System.Collections.Generic;

public class PortraitDatabase : MonoBehaviour
{
    public static PortraitDatabase Instance;

    // id（女1） → expression（常態） → sprite
    private Dictionary<string, Dictionary<string, Sprite>> data
        = new Dictionary<string, Dictionary<string, Sprite>>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ⭐ 建議保留（跨場景）
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadAllSprites();
    }

    // ⭐ 載入全部立繪（男女分資料夾）
    void LoadAllSprites()
    {
        data.Clear();

        Sprite[] males = Resources.LoadAll<Sprite>("Character illustration/Male");
        Sprite[] females = Resources.LoadAll<Sprite>("Character illustration/Female");

        Debug.Log("Male數量: " + males.Length);
        Debug.Log("Female數量: " + females.Length);

        LoadSprites(males);
        LoadSprites(females);

        Debug.Log("✅ 立繪資料庫載入完成，角色數：" + data.Count);

        if (data.Count == 0)
        {
            Debug.LogError("❌ 沒有載入任何立繪！請檢查 Resources 路徑或圖片設定");
        }
    }

    // ⭐ 解析 Sprite（核心）
    void LoadSprites(Sprite[] sprites)
    {
        foreach (var sprite in sprites)
        {
            if (sprite == null) continue;

            // 格式：女1_常態
            string[] split = sprite.name.Split('_');

            if (split.Length < 2)
            {
                Debug.LogWarning("⚠️ 命名不符合規則：" + sprite.name);
                continue;
            }

            string id = split[0];         // 女1
            string expression = split[1]; // 常態 / 生氣

            if (!data.ContainsKey(id))
            {
                data[id] = new Dictionary<string, Sprite>();
            }

            data[id][expression] = sprite;

            // Debug（可關）
            // Debug.Log($"載入：{id}_{expression}");
        }
    }

    // ⭐ 取得立繪（最重要API）
    public Sprite GetPortrait(string id, string expression)
    {
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogError("❌ GetPortrait：id 是空的");
            return null;
        }

        if (!data.ContainsKey(id))
        {
            Debug.LogError("❌ 找不到角色ID：" + id);
            return null;
        }

        // ✔ 正常取得
        if (data[id].ContainsKey(expression))
        {
            return data[id][expression];
        }

        // ⭐ fallback → 常態
        if (data[id].ContainsKey("常態"))
        {
            Debug.LogWarning($"⚠️ 找不到表情 {expression}，改用常態");
            return data[id]["常態"];
        }

        Debug.LogError($"❌ {id} 沒有任何可用立繪");
        return null;
    }

    // ⭐ 取得全部角色ID
    public List<string> GetAllCharacterIDs()
    {
        return new List<string>(data.Keys);
    }

    // ⭐ 依性別取得（用字串判斷版本）
    public List<string> GetCharacterIDsByGender(Gender gender)
    {
        List<string> result = new List<string>();
        Debug.Log("GetCharacterIDsByGender:"+gender);
        foreach (var id in data.Keys)
        {
            if (gender == Gender.Male && id.Contains("男"))
            {
                result.Add(id);
            }

            if (gender == Gender.Female && id.Contains("女"))
            {
                result.Add(id);
            }
        }

        return result;
    }
}