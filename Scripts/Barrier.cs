using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    public Transform player;
    // volume settings for sound effects
    public float maxVolume = 1.0f;
    public float minVolume = 0.1f;
    public float maxDistance = 10.0f;

    private AudioSource audioSource;
    public AudioClip barrierSound;
    private bool soundPlaying = false;

    void Start()
    {
        // Ensure AudioSource exists
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Assign clip
        if (barrierSound != null)
            audioSource.clip = barrierSound;

        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.Stop();

        // Start sound effect after 5 seconds
        StartCoroutine(PlayBarrierSoundAfterDelay(3.0f));
    }

    IEnumerator PlayBarrierSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource.clip != null)
        {
            audioSource.Play();
            soundPlaying = true;
        }
    }

    void Update()
    {
        if (player != null && soundPlaying)
        {
            // distance between barrier and player
            float distance = Vector2.Distance(transform.position, player.position);

            // clamp volume between min and max
            float volume = Mathf.Clamp01(1f - (distance / maxDistance));
        
            // ensure volume is above minimum
            audioSource.volume = Mathf.Max(volume, minVolume) * maxVolume;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // destroy any gem that the barrier touches
        if (other.CompareTag("XPGems") ||
            other.CompareTag("BonusGems") ||
            other.CompareTag("GhostGems") ||
            other.CompareTag("SpeedGems") ||
            other.CompareTag("Player") ||
            other.CompareTag("Detonator"))
        {
            Destroy(other.gameObject);
        }
    }
}
