using UnityEngine;

public class BrickScore : MonoBehaviour
{
    public int points = 10;
    bool alreadyScored;

    public AudioSource audioSource;
    public AudioClip hitSound;
    public float minVelocity = 0.2f;
    public float volumeMultiplier = 0.1f;

    void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        // scoring logic
        if (collision.collider.CompareTag("Ball"))
        {
            if (!alreadyScored)
            {
                alreadyScored = true;
                if (ScoreManager.Instance != null)
                    ScoreManager.Instance.AddScore(points);
            }
        }

        // sound logic
        if (hitSound != null && audioSource != null)
        {
            if (impactForce >= minVelocity)
            {
                float volume = Mathf.Clamp01(impactForce * volumeMultiplier);
                audioSource.PlayOneShot(hitSound, volume);
            }
        }
    }
}
