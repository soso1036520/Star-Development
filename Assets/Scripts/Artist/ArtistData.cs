using System;
using UnityEngine;

[Serializable]
public class ArtistData
{
    public string artistName;
    public int age;
    public Gender gender;

    public BackgroundType background;
    public PersonalityType personality;
    public SpecialtyType specialty;

    public int acting;
    public int singing;
    public int variety;
    public int charm;

    public int popularity;
    public int stamina;

    public int signingFee;
    public DateTime signDate;

    public int contractWeeks;

    public Sprite portrait;
    public string characterID;   // 女1
    public string expression;    // 常態 / 生氣
}

public enum Gender
{
    Male,
    Female
}

public enum BackgroundType
{
    Newbie,        // 社會新人
    Trainee,       // 練習生
    Influencer,    // 網紅
    StreetPerformer
}

public enum PersonalityType
{
    Calm,
    HotTempered,   // 火爆
    Lazy,
    Friendly,
    Perfectionist
}

public enum SpecialtyType
{
    Acting,
    Singing,
    Variety
}