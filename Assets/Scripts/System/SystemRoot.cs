using UnityEngine;

public class SystemRoot : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsOfType<SystemRoot>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // ⭐ 載入主選單
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }
}