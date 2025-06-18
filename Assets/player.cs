using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    private bool gameStarted = false;
    public  float speed = 4f, jumpForce = 10f;
    public bool dead, jumping;

    public TextMeshProUGUI gameOverText;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;

    public bool isTouchingWall;
    [SerializeField] private Transform leftChecker;
    [SerializeField] private Transform rightChecker;
    [SerializeField] private Transform jumpChecker;
    private LayerMask groundMask;

    private Collider2D leftCollider;
    private Collider2D rightCollider;
    private Collider2D jumpCollider;

    public AudioSource jumpSound;

    private bool moveLeft, moveRight, jump;

    private bool hasShownGameOver = false;

    private bool isOnGround;

    public AudioClip boomSound;
    private AudioSource audioSource;

    void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();

        leftCollider = leftChecker.GetComponent<Collider2D>();
        rightCollider = rightChecker.GetComponent<Collider2D>();
        jumpCollider = jumpChecker.GetComponent<Collider2D>();
        groundMask = LayerMask.GetMask("ground");

        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (dead)
        {
            Death();

            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            return;
        }
        Movement();
    }

    void Movement()
    {
        CheckSurroundings();

        float move = 0;
        if (moveLeft) move = -1;
        else if (moveRight) move = 1;

        if (isTouchingWall && jumping)
        {
            move = 0;
        }

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        if (move != 0)
        {
            sp.flipX = move < 0;
        }

        bool isJumping = rb.linearVelocity.y > 0.1f;
        bool isFalling = rb.linearVelocity.y < -0.1f;
        bool isRunning = move != 0 && isOnGround;

        // Ưu tiên anim nhảy/rơi trước
        // Gán anim theo độ ưu tiên: Jumping > Falling > Running > Idle
        anim.SetBool("Jumping", isJumping);
        anim.SetBool("Falling", isFalling);
        anim.SetBool("Running", isRunning);
    }


    private void CheckSurroundings()
    {
        LayerMask groundMask = LayerMask.GetMask("ground");
        isTouchingWall = leftCollider.IsTouchingLayers(groundMask) || rightCollider.IsTouchingLayers(groundMask);
        isOnGround = jumpCollider.IsTouchingLayers(groundMask);

        if (isOnGround)
        {
            jumping = false;
        }
        else
        {
            jumping = true;
        }
    }

    public void Jumping()
    {
        if (!jumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpSound?.Play();
        }
    }

    public void MoveLeft() => moveLeft = true;
    public void StopMoveLeft() => moveLeft = false;

    public void MoveRight() => moveRight = true;
    public void StopMoveRight() => moveRight = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("boom"))
        {
            if (dead) return;
            dead = true;

            if (audioSource != null && boomSound != null)
            {
                audioSource.PlayOneShot(boomSound);
            }
        }
    }


    void Death()
    {
        if (!hasShownGameOver)
        {
            anim.SetTrigger("Dead");
            gameOverText.text = "Game Over!\n<size=70>Tap to Play Again!</size>";
            hasShownGameOver = true;
            StartCoroutine(WaitAndPause());
        }
    }

    IEnumerator WaitAndPause()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
    }
}
