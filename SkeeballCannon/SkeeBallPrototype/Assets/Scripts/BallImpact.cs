using UnityEngine;

public class BallImpact : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip impactSound;
    public float minVelocity = 1f;
    public float volumeMultiplier = 0.1f;

    void OnCollisionEnter(Collision collision)
    {
        if (impactSound == null || audioSource == null)
            return;

        float impactForce = collision.relativeVelocity.magnitude;
        if (impactForce < minVelocity)
            return;

        float volume = Mathf.Clamp01(impactForce * volumeMultiplier);
        audioSource.PlayOneShot(impactSound, volume);
    }
}
