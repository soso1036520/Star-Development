using UnityEngine;
//離開遊戲
public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("離開遊戲");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 在Unity內停止
        #else
        Application.Quit(); // 打包後關閉遊戲
        #endif
    }
}