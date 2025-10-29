using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Player : MonoBehaviour
{

    // gem counter
    public int gems = 0;
    public int speedGems = 0;
    public int ghostGems = 0;
    public int shrinkGems = 0;

    // speed of the player
    public float speed = 3.0f;
    private float fastSpeed;

    // text to display in inspector
    public TextMeshProUGUI gemAmount;
    public TextMeshProUGUI speedGemAmount;
    public TextMeshProUGUI ghostGemAmount;
    public TextMeshProUGUI shrinkGemAmount;

    // Sound effects for each gems
    public AudioClip xpGemSound;
    public AudioClip bonusGemSound;
    public AudioClip speedGemSound;
    public AudioClip ghostGemSound;
    public AudioClip shrinkGemSound;
    public AudioClip winSound;
    public AudioClip barrierSound;
    private AudioSource audioSource;

    // Event for player death
    public static event Action OnPlayerDeath;

    // Event for player victory
    public static event Action OnPlayerVictory;

    // Rigidbody2D component
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Power usage durations
    private bool isSpeedActive = false;
    private bool isGhostActive = false;
    private bool isShrinkActive = false;
    public float speedPowerDuration = 8.0f;
    public float ghostPowerDuration = 5.0f;
    public float shrinkPowerDuration = 10.0f;

    private Collider2D playerCollider;

    // For changing sprite transparency
    private SpriteRenderer spriteRenderer;

    // Check death state
    private bool isDead = false;

    // Enable player movement
    private void OnEnable()
    {
        Player.OnPlayerDeath += DisablePlayerMovement;
    }

    // Disable player movement
    private void OnDisable()
    {
        Player.OnPlayerDeath -= DisablePlayerMovement;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        fastSpeed = speed;

        // initialize the gem counter text
        if (gemAmount != null)
            gemAmount.text = "Gems: " + gems;

        if (speedGemAmount != null)
            speedGemAmount.text = "Speed: " + speedGems;

        if (ghostGemAmount != null)
            ghostGemAmount.text = "Ghost: " + ghostGems;

        if (shrinkGemAmount != null)
            shrinkGemAmount.text = "Shrink: " + shrinkGems;

        // Call enable player movement at start of game
        EnablePlayerMovement();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Vector2.zero;

        // keyboard movement input
        if (Input.GetKey(KeyCode.LeftArrow))
            moveInput.x = -1;
        if (Input.GetKey(KeyCode.RightArrow))
            moveInput.x = 1;
        if (Input.GetKey(KeyCode.UpArrow))
            moveInput.y = 1;
        if (Input.GetKey(KeyCode.DownArrow))
            moveInput.y = -1;

        moveInput = moveInput.normalized;

        // Activate Speed Power
        if (Input.GetKeyDown(KeyCode.Alpha1) && speedGems > 0 && !isSpeedActive && !isGhostActive && !isShrinkActive)
        {
            StartCoroutine(ActivateSpeedPower());
        }

        // Activate Ghost Power
        if (Input.GetKeyDown(KeyCode.Alpha2) && ghostGems > 0 && !isGhostActive && !isSpeedActive && !isShrinkActive)
        {
            StartCoroutine(ActivateGhostPower());
        }
        
        // Activate Shrink Power
        if (Input.GetKeyDown(KeyCode.Alpha3) && shrinkGems > 0 && !isGhostActive && !isSpeedActive && !isShrinkActive)
        {
            StartCoroutine(ActivateShrinkPower());
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    // Disable player movement after death
    private void DisablePlayerMovement()
    {
        isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
    }

    // Enable player movement at start of game
    private void EnablePlayerMovement()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collecting the XPGems on collision
        if (collision.gameObject.tag == "XPGems")
        {
            gems += 10;
            gemAmount.text = "Gems: " + gems;
            audioSource.PlayOneShot(xpGemSound);
            Destroy(collision.gameObject);
        }

        // collecting the BonusGems on collision
        else if (collision.gameObject.tag == "BonusGems")
        {
            gems += 50;
            gemAmount.text = "Gems: " + gems;
            audioSource.PlayOneShot(bonusGemSound);
            Destroy(collision.gameObject);
        }

        // collecting the SpeedGems on collision
        else if (collision.gameObject.tag == "SpeedGems")
        {
            speedGems += 1;
            speedGemAmount.text = "Speed: " + speedGems;
            audioSource.PlayOneShot(speedGemSound);
            Destroy(collision.gameObject);
        }

        // collecting the GhostGems on collision
        else if (collision.gameObject.tag == "GhostGems")
        {
            ghostGems += 1;
            ghostGemAmount.text = "Ghost: " + ghostGems;
            audioSource.PlayOneShot(ghostGemSound);
            Destroy(collision.gameObject);
        }

        // collecting the ShrinkGems on collision
        else if (collision.gameObject.tag == "ShrinkGems")
        {
            shrinkGems += 1;
            shrinkGemAmount.text = "Shrink: " + shrinkGems;
            audioSource.PlayOneShot(shrinkGemSound);
            Destroy(collision.gameObject);
        }

        // collecting the WinHelicopter on collision
        else if (collision.gameObject.tag == "WinHelicopter")
        {
            audioSource.PlayOneShot(winSound);
            OnPlayerVictory?.Invoke();
        }

        // Barrier collides with the player
        else if (collision.gameObject.tag == "Barrier")
        {
            audioSource.PlayOneShot(barrierSound);
            OnPlayerDeath?.Invoke();
        }
    }

    // Handle usage of speed powers
    private IEnumerator ActivateSpeedPower()
    {
        isSpeedActive = true;
        speedGems -= 1;
        speedGemAmount.text = "Speed: " + speedGems;
        speed = fastSpeed * 2.5f;
        yield return new WaitForSeconds(speedPowerDuration);
        // change speed back to normal after duration
        speed = fastSpeed;
        isSpeedActive = false;
    }

    // Handle usage of ghost powers
    private IEnumerator ActivateGhostPower()
    {
        isGhostActive = true;
        ghostGems -= 1;
        ghostGemAmount.text = "Ghost: " + ghostGems;
        // Disable collision with walls
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Walls"), true);
        // Change sprite transparency to indicate ghost mode
        Color c = spriteRenderer.color;
        // 40% opaque
        c.a = 0.4f;
        spriteRenderer.color = c;
        yield return new WaitForSeconds(ghostPowerDuration);
        // change character back to normal
        c.a = 1.0f;
        spriteRenderer.color = c;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Walls"), false);
        isGhostActive = false;
    }

    private IEnumerator ActivateShrinkPower()
    {
        isShrinkActive = true;
        shrinkGems -= 1;
        shrinkGemAmount.text = "Shrink: " + shrinkGems;
        Vector3 originalScale = transform.localScale;
        // shrink character by 50%
        transform.localScale = originalScale * 0.5f;
        // shrink duration time
        yield return new WaitForSeconds(shrinkPowerDuration);
        // change size back to normal
        transform.localScale = originalScale;
        isShrinkActive = false;
    }
}
