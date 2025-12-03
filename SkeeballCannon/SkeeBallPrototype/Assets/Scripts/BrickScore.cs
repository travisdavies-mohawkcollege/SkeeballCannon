using UnityEngine;

public class BrickScore : MonoBehaviour
{
    public int points = 10;
    bool alreadyScored;

    void OnCollisionEnter(Collision collision)
    {
        // only react to the cannonball
        if (!collision.collider.CompareTag("SkeeBall"))
            return;

        if (alreadyScored)
            return;

        alreadyScored = true;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(points);
            Debug.Log($"BrickScore: +{points} points from {name}");
        }
    }
}
