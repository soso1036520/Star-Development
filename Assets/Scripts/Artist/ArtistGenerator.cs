using System;
using UnityEngine;
//隨機生成
public static class ArtistGenerator
{
    static string[] maleNames = { "陳宇", "林哲", "張凱", "王辰" };
    static string[] femaleNames = { "蘇晴", "林依", "周然", "葉彤" };

    public static ArtistData GenerateRandomArtist(Sprite portrait = null)
    {
        ArtistData data = new ArtistData();
        // 性別
        data.gender = (UnityEngine.Random.value > 0.5f) ? Gender.Male : Gender.Female;

        // 名字依性別
        if (data.gender == Gender.Male)
            data.artistName = maleNames[UnityEngine.Random.Range(0, maleNames.Length)];
        else
            data.artistName = femaleNames[UnityEngine.Random.Range(0, femaleNames.Length)];

        data.age = UnityEngine.Random.Range(18, 30);
        

        // 背景 / 個性 / 擅長
        data.background = GetRandomEnum<BackgroundType>();
        data.personality = GetRandomEnum<PersonalityType>();
        data.specialty = GetRandomEnum<SpecialtyType>();

        GenerateStatsBySpecialty(data);

        data.popularity = UnityEngine.Random.Range(10, 40);
        data.stamina = 100;

        data.signingFee = CalculateSigningFee(data);
        data.signDate = DateTime.Now;
        data.contractWeeks = 12;

        data.portrait = portrait;

        return data;
    }

    static void GenerateStatsBySpecialty(ArtistData data)
    {
        data.acting = UnityEngine.Random.Range(40, 70);
        data.singing = UnityEngine.Random.Range(40, 70);
        data.variety = UnityEngine.Random.Range(40, 70);
        data.charm = UnityEngine.Random.Range(50, 80);

        switch (data.specialty)
        {
            case SpecialtyType.Acting:
                data.acting += 20;
                break;
            case SpecialtyType.Singing:
                data.singing += 20;
                break;
            case SpecialtyType.Variety:
                data.variety += 20;
                break;
        }
    }

    static int CalculateSigningFee(ArtistData data)
    {
        int baseFee = 20000;

        // 背景影響
        switch (data.background)
        {
            case BackgroundType.Newbie:
                baseFee += 10000;
                break;
            case BackgroundType.Trainee:
                baseFee += 20000;
                break;
            case BackgroundType.Influencer:
                baseFee += 40000;
                break;
        }

        // 能力加成
        int totalStats = data.acting + data.singing + data.variety + data.charm;
        baseFee += totalStats * 50;

        return baseFee;
    }

    static T GetRandomEnum<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }
}