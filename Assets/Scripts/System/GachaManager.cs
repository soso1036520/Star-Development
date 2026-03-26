using UnityEngine;
using System.Collections.Generic;

public class GachaManager : MonoBehaviour
{
    public ContractUI contractUI;
    public CompanyData companyData;

    private Sprite[] maleSprites;
    private Sprite[] femaleSprites;
    public ArtistData currentArtist;

    void Start()
    {
        // 先抓全部
        Sprite[] allMale = Resources.LoadAll<Sprite>("Character illustration/Male");
        Sprite[] allFemale = Resources.LoadAll<Sprite>("Character illustration/Female");

        // ⭐ 過濾（只留「常態」）
        maleSprites = FilterNormalSprites(allMale);
        femaleSprites = FilterNormalSprites(allFemale);

        Debug.Log("男常態數量：" + maleSprites.Length);
        Debug.Log("女常態數量：" + femaleSprites.Length);
    }

    public void DrawArtist()
    {
        // ⭐ 檢查資料庫
        if (PortraitDatabase.Instance == null)
        {
            Debug.LogError("❌ PortraitDatabase 不存在！");
            return;
        }

        // ⭐ 檢查UI
        if (contractUI == null)
        {
            Debug.LogError("❌ contractUI 沒設定！");
            return;
        }

        // ⭐ 產生藝人
        ArtistData artist = ArtistGenerator.GenerateRandomArtist();

        if (artist == null)
        {
            Debug.LogError("❌ Artist 生成失敗！");
            return;
        }

        // ⭐ 依性別取得角色ID
        List<string> ids = PortraitDatabase.Instance
            .GetCharacterIDsByGender(artist.gender);

        if (ids == null || ids.Count == 0)
        {
            Debug.LogError($"❌ 沒有可用角色（性別：{artist.gender}）");
            return;
        }

        // ⭐ 隨機角色
        int index = Random.Range(0, ids.Count);
        artist.characterID = ids[index];

        // ⭐ 預設表情
        artist.expression = "常態";

        // ⭐ 取得立繪
        Sprite portrait = PortraitDatabase.Instance
            .GetPortrait(artist.characterID, artist.expression);

        if (portrait == null)
        {
            Debug.LogError($"❌ 找不到立繪：{artist.characterID}_{artist.expression}");
            return;
        }

        artist.portrait = portrait;
        currentArtist = artist; // ⭐這行一定要有

        // ⭐ 丟給UI
        contractUI.Init(artist, companyData);

        // Debug（可留）
        Debug.Log($"✅ 抽到角色：{artist.characterID}（{artist.gender}）");
    }
    Sprite[] FilterNormalSprites(Sprite[] allSprites)
    {
        List<Sprite> result = new List<Sprite>();

        foreach (var sprite in allSprites)
        {
            // ⭐ 關鍵：只抓「常態」
            if (sprite.name.Contains("常態"))
            {
                result.Add(sprite);
            }
        }

        return result.ToArray();
    }
}