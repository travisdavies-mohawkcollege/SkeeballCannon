using System.Collections.Generic;
using UnityEngine;

public class SkeeBallReplay : MonoBehaviour
{
    public float maxRecordTime = 5f;
    public float minSpeedToRecord = 0.1f;

    List<Vector3> positions;
    List<Quaternion> rotations;

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
        rotations = new List<Quaternion>();

        recordTimer = 0f;
        replayTimer = 0f;

        IsRecording = true;
        IsReplaying = false;
    }

    public void StartReplay()
    {
        if (positions == null || positions.Count == 0)
            return;

        IsRecording = false;
        IsReplaying = true;
        replayTimer = 0f;

        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    public void StopReplay()
    {
        IsReplaying = false;

        if (rb)
            rb.isKinematic = false;
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
        rotations.Add(transform.rotation);

        if (recordTimer >= maxRecordTime)
            IsRecording = false;
    }

    void ReplayStep()
    {
        replayTimer += Time.fixedDeltaTime;

        float stepTime = Time.fixedDeltaTime;
        int index = Mathf.FloorToInt(replayTimer / stepTime);

        if (index >= 0 && index < positions.Count)
        {
            transform.position = positions[index];
            transform.rotation = rotations[index];
        }
        else
        {
            StopReplay();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (IsRecording)
            IsRecording = false;
    }
}
