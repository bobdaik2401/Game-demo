using UnityEngine;
using System.Collections;

public class FallTrigger : MonoBehaviour
{

    [Header("Các object cần rơi")]
    public GameObject[] fallObjects;
    public float[] fallDelays;

    [Header("Âm thanh rơi")]
    public AudioClip fallSound;
    public float volume = 1f;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true;
            StartFalling();
        }
    }

    void StartFalling()
    {
        for (int i = 0; i < fallObjects.Length; i++)
        {
            GameObject obj = fallObjects[i];
            float delay = (i < fallDelays.Length) ? fallDelays[i] : 0f;

            if (obj != null)
            {
                StartCoroutine(DelayFall(obj, delay));
            }
        }
    }

    IEnumerator DelayFall(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

            if (fallSound != null)
            {
                GameObject tempAudio = new GameObject("TempFallSound");
                AudioSource audio = tempAudio.AddComponent<AudioSource>();
                audio.clip = fallSound;
                audio.volume = volume;
                audio.Play();
                Destroy(tempAudio, fallSound.length);
            }
        }
    }

}
