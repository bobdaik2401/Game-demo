using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    public float delayBeforeLoadingScene = 0.5f; // Thời gian trì hoãn

    void Awake()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;

        if (audioSource != null)
        {
            audioSource.Stop(); // Dừng nếu đang phát
            audioSource.clip = null;         // Xóa clip gán sẵn
            audioSource.playOnAwake = false; // Tắt tự phát
            audioSource.volume = 0;
        }
    }
    void Start()
    {
        if (audioSource != null)
        {
            StartCoroutine(RestoreVolume());
        }
    }


    public void batdau()
    {
        PlayerPrefs.DeleteKey("LevelProgress");
        PlayerPrefs.Save();
        PlayButtonClickSound();
        StartCoroutine(LoadSceneAfterDelay("level1"));
    }
    public void ChoiTiep()
    {
        int progress = PlayerPrefs.GetInt("LevelProgress", 1); // Mặc định là Level 1
        string sceneName = "level" + progress;
        PlayButtonClickSound();
        StartCoroutine(LoadSceneAfterDelay(sceneName));
    }
    public void chapter()
    {
        PlayButtonClickSound();
        StartCoroutine(LoadSceneAfterDelay("chapter")); // Sử dụng tên scene thay vì số
    }

    public void loa()
    {
        PlayButtonClickSound();
        // Không cần chuyển cảnh
    }

    public void thoat()
    {
        PlayButtonClickSound();
        StartCoroutine(QuitAfterDelay());
    }

    public void thoatramenuchinh()
    {
        PlayButtonClickSound();
        StartCoroutine(LoadSceneAfterDelay("menu")); // Dùng tên scene "menu"
    }

    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    // Coroutine để trì hoãn việc chuyển cảnh theo tên
    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeLoadingScene);
        SceneManager.LoadScene(sceneName); 
    }

    // Coroutine để trì hoãn thoát ứng dụng
    private IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoadingScene);
        Application.Quit();
    }
    private IEnumerator RestoreVolume()
    {
        yield return null;
        audioSource.volume = 1;
    }
}
