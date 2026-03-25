using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Transform target;          // 要縮放的UI（你的StartText）
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private Vector3 originalScale;

    void Start()
    {
        if (target == null)
            target = transform;

        originalScale = target.localScale;
    }

    // 滑鼠進入
    public void OnPointerEnter(PointerEventData eventData)
    {
        target.localScale = originalScale * 1.05f;
    }

    // 滑鼠離開
    public void OnPointerExit(PointerEventData eventData)
    {
        target.localScale = originalScale;
    }

    // 按下
    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }

    // 放開
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}