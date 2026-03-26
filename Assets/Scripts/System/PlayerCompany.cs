using System.Collections.Generic;
using UnityEngine;
public class PlayerCompany : MonoBehaviour
{
    private static PlayerCompany _instance;

    public static PlayerCompany Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogWarning("⚠️ PlayerCompany 不存在，自動建立");

                GameObject obj = new GameObject("PlayerCompany");
                _instance = obj.AddComponent<PlayerCompany>();
            }
            return _instance;
        }
    }

    public PlayerData player;
    public List<ArtistData> artists = new List<ArtistData>();

    void Awake()
    {
        if (_instance == null)
            _instance = this;

        // ⭐ 保底初始化
        if (player == null)
        {
            player = new PlayerData()
            {
                playerID = "1p",
                playerName = "玩家"
            };
        }
    }
}