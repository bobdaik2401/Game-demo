using UnityEngine;

public class Grounddown : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public AudioClip trapSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Đảm bảo rb không null và chỉ thay đổi bodyType nếu chưa phải Dynamic
            if (rb != null && rb.bodyType != RigidbodyType2D.Dynamic)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }

            // Phát âm thanh
            if (audioSource != null && trapSound != null)
            {
                audioSource.PlayOneShot(trapSound);
            }
        }

    }
}
