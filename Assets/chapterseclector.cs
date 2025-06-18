using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChapterSelector : MonoBehaviour
{
    [System.Serializable]
    public class ChapterButton
    {
        public Button button;
        public Image buttonImage;
        public Text buttonText;
        public string sceneToLoad;
    }

    public ChapterButton[] chapters; // Mỗi phần tử là 1 level
    public Sprite lockedSpriteGlobal;
    public Sprite unlockedSpriteGlobal;

    void Start()
    {
        // Gán tạm tiến độ cao để test hiển thị tất cả button (nếu muốn)
        // PlayerPrefs.SetInt("LevelProgress", chapters.Length); // Bỏ comment để test
        // PlayerPrefs.Save();

        int progress = PlayerPrefs.GetInt("LevelProgress", 1); // Mặc định bắt đầu từ Level 1
        Debug.Log("Level Progress hiện tại: " + progress);

        for (int i = 0; i < chapters.Length; i++)
        {
            if (chapters[i] == null || chapters[i].button == null || chapters[i].buttonImage == null)
            {
                Debug.LogWarning($"ChapterButton tại index {i} chưa được gán đúng trong Inspector.");
                continue;
            }

            bool unlocked = (i + 1) <= progress;
            SetChapterButtonState(i, unlocked);
        }

        // Kiểm tra Sprite
        if (lockedSpriteGlobal == null || unlockedSpriteGlobal == null)
        {
            Debug.LogWarning("⚠️ Chưa gán sprite khóa/mở khóa trong Inspector.");
        }
    }

    void SetChapterButtonState(int chapterIndex, bool unlocked)
    {
        if (chapterIndex < 0 || chapterIndex >= chapters.Length) return;

        ChapterButton chapter = chapters[chapterIndex];
        chapter.button.interactable = unlocked;

        if (chapter.buttonImage != null)
        {
            chapter.buttonImage.sprite = unlocked ? unlockedSpriteGlobal : lockedSpriteGlobal;
            chapter.buttonImage.color = unlocked ? Color.white : Color.gray;
        }

        if (chapter.buttonText != null)
        {
            chapter.buttonText.gameObject.SetActive(unlocked);
            chapter.buttonText.text = unlocked ? chapter.sceneToLoad : "";
        }

        chapter.button.onClick.RemoveAllListeners();

        if (unlocked)
        {
            string scene = chapter.sceneToLoad;
            chapter.button.onClick.AddListener(() => LoadChapter(scene));
        }
    }

    void LoadChapter(string sceneName)
    {
        Debug.Log("Đang tải scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
