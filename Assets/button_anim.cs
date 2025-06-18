using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSpriteController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image buttonImage;
    public Sprite pressedSprite;
    public Sprite defaultSprite;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();
    }

    // Khi nhấn button
    public void OnPointerDown(PointerEventData eventData)
    {
        // Đổi sang Sprite khi nhấn
        buttonImage.sprite = pressedSprite;
    }

    // Khi thả button
    public void OnPointerUp(PointerEventData eventData)
    {
        // Quay lại Sprite mặc định
        buttonImage.sprite = defaultSprite;
    }
}
