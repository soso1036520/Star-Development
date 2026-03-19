using UnityEngine;
using System.Collections.Generic;

public class GachaManager : MonoBehaviour
{
    public ContractUI contractUI;
    public CompanyData companyData;

    private Sprite[] maleSprites;
    private Sprite[] femaleSprites;

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
        ArtistData artist = ArtistGenerator.GenerateRandomArtist();

        // ⭐ 根據性別給圖
        if (artist.gender == Gender.Male && maleSprites.Length > 0)
        {
            artist.portrait = maleSprites[Random.Range(0, maleSprites.Length)];
        }
        else if (artist.gender == Gender.Female && femaleSprites.Length > 0)
        {
            artist.portrait = femaleSprites[Random.Range(0, femaleSprites.Length)];
        }

        contractUI.Init(artist, companyData);
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