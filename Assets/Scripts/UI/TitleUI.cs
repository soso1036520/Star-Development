using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void OnClickStart()
    {
        Debug.Log("開始遊戲");
        SceneManager.LoadScene("StoryScene");
    }
}