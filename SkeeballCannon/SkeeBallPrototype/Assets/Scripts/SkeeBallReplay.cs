using System.Collections.Generic;
using UnityEngine;

public class SkeeBallReplay : MonoBehaviour
{
    public float maxRecordTime = 5f;
    public float replayDuration = 5f;
    public float minSpeedToRecord = 0.1f;

    List<Vector3> positions;

    public bool IsRecording { get; private set; }
    public bool IsReplaying { get; private set; }

    float recordTimer;
    float replayTimer;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void StartRecording()
    {
        positions = new List<Vector3>();

        recordTimer = 0f;
        replayTimer = 0f;

        IsRecording = true;
        IsReplaying = false;
    }

    public void StartReplay()
    {
        if (positions == null || positions.Count == 0)
        {
            Debug.LogWarning("Replay: no recorded positions, cannot start replay");
            return;
        }

        IsRecording = false;
        IsReplaying = true;
        replayTimer = 0f;

        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        Debug.Log("Replay: START");
    }

    public void StopReplay()
    {
        IsReplaying = false;

        if (rb)
            rb.isKinematic = false;

        Debug.Log("Replay: STOP");
    }

    void FixedUpdate()
    {
        if (IsRecording)
            RecordStep();
        else if (IsReplaying)
            ReplayStep();
    }

    void RecordStep()
    {
        if (rb && rb.velocity.magnitude < minSpeedToRecord)
        {
            IsRecording = false;
            return;
        }

        recordTimer += Time.fixedDeltaTime;
        positions.Add(transform.position);

        if (recordTimer >= maxRecordTime)
            IsRecording = false;
    }

    void ReplayStep()
    {
        replayTimer += Time.fixedDeltaTime;

        float t = Mathf.Clamp01(replayTimer / replayDuration);

        int lastIndex = positions.Count - 1;
        if (lastIndex <= 0)
        {
            StopReplay();
            return;
        }

        float floatIndex = t * lastIndex;
        int indexA = Mathf.FloorToInt(floatIndex);
        int indexB = Mathf.Min(indexA + 1, lastIndex);

        float lerpT = floatIndex - indexA;

        Vector3 a = positions[indexA];
        Vector3 b = positions[indexB];

        transform.position = Vector3.Lerp(a, b, lerpT);

        if (replayTimer >= replayDuration)
            StopReplay();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (IsRecording)
            IsRecording = false;
    }
}
