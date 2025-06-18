using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Image soundIcon;          // Biểu tượng loa trên UI
    public Sprite mutedSprite;       // Hình ảnh loa tắt
    public Sprite unmutedSprite;     // Hình ảnh loa bật

    void Start()
    {
        // Kiểm tra trạng thái âm thanh đã được lưu trước đó
        if (PlayerPrefs.HasKey("Muted"))
        {
            bool isMuted = PlayerPrefs.GetInt("Muted") == 1;
            AudioListener.volume = isMuted ? 0 : 1;
        }
        else
        {
            // Nếu không có giá trị lưu, mặc định âm thanh bật
            PlayerPrefs.SetInt("Muted", 0);
            AudioListener.volume = 1;
        }

        // Cập nhật biểu tượng loa khi bắt đầu
        UpdateSoundIcon();
    }

    // Hàm bật/tắt âm thanh khi người chơi nhấn vào biểu tượng loa
    public void ToggleSound()
    {
        bool isMuted = AudioListener.volume == 0;

        if (isMuted)
        {
            AudioListener.volume = 1;               // Bật âm thanh
            PlayerPrefs.SetInt("Muted", 0);         // Lưu trạng thái âm thanh không tắt
        }
        else
        {
            AudioListener.volume = 0;               // Tắt âm thanh
            PlayerPrefs.SetInt("Muted", 1);         // Lưu trạng thái âm thanh bị tắt
        }

        // Cập nhật lại biểu tượng loa
        UpdateSoundIcon();
    }

    // Cập nhật biểu tượng loa trên UI
    private void UpdateSoundIcon()
    {
        bool isMuted = AudioListener.volume == 0;
        soundIcon.sprite = isMuted ? mutedSprite : unmutedSprite;
    }
}
