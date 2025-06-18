using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions; // Dùng để tách số level từ tên scene

public class Win : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;
    public AudioClip winSound;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (anim != null)
            {
                anim.SetBool("Win", true);
            }

            collision.gameObject.SetActive(false);

            if (audioSource != null && winSound != null)
            {
                audioSource.PlayOneShot(winSound);
            }

            SaveProgressIfNeeded(); // Ghi tiến độ level đã hoàn thành
            StartCoroutine(DelayedAction());
        }
    }

    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(2);

        string nextLevelName = GetNextLevelName();
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("Không tìm thấy level tiếp theo.");
        }
    }


    void SaveProgressIfNeeded()
    {
        // Lấy tên scene hiện tại thay vì sceneName
        string currentSceneName = SceneManager.GetActiveScene().name;

        Match match = Regex.Match(currentSceneName, @"level(\d+)", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            int currentLevel = int.Parse(match.Groups[1].Value);
            int savedProgress = PlayerPrefs.GetInt("LevelProgress", 1); // Mặc định là level 1

            if (currentLevel >= savedProgress)
            {
                PlayerPrefs.SetInt("LevelProgress", currentLevel + 1); // Mở khóa level tiếp theo
                PlayerPrefs.Save();
                Debug.Log("Đã lưu tiến độ: Level " + (currentLevel + 1));
            }
        }
        else
        {
            Debug.LogWarning("Không tách được số level từ tên scene hiện tại.");
        }
    }
    string GetNextLevelName()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Match match = Regex.Match(currentSceneName, @"level(\d+)", RegexOptions.IgnoreCase);

        if (match.Success)
        {
            int currentLevel = int.Parse(match.Groups[1].Value);
            int nextLevel = currentLevel + 1;
            string nextSceneName = "Level" + nextLevel;

            // Kiểm tra xem scene có tồn tại không trước khi load (tránh crash)
            if (Application.CanStreamedLevelBeLoaded(nextSceneName))
            {
                return nextSceneName;
            }
            else
            {
                Debug.LogWarning("Scene '" + nextSceneName + "' không tồn tại hoặc chưa được thêm vào Build Settings.");
                return null;
            }
        }

        return null;
    }
}
