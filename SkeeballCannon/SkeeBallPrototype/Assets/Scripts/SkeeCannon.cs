using UnityEngine;
using UnityEngine.UI;

public class SkeeCannon : MonoBehaviour
{
    [Header("Ball Physics")]
    public GameObject ballPrefab;
    public Transform firePoint;
    public float projectileMassLbs = 12f;
    const float poundsToKg = 0.45359237f;
    public float muzzleVelocity = 30f;      // base speed
    public float powerMultiplier = 4f;      // extra punch

    [Header("Cooldown")]
    public float cooldownTime = 2f;
    float cooldownTimer = 0f;               // 0 = ready
    public Slider cooldownSlider;           // 1 = ready, 0 = cooling

    [Header("Aim")]
    public float mouseSensitivity = 3f;
    public float minPitch = -30f;
    public float maxPitch = 45f;
    float yaw;
    float pitch;

    [Header("Effects")]
    public ParticleSystem fireSmoke;
    public AudioSource fireAudio;

    [Header("Cameras")]
    public Camera mainCamera;
    public Camera replayCamera;
    public ReplayCameraController replayCamController;

    GameObject lastBall;
    SkeeBallReplay lastReplay;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 start = transform.eulerAngles;
        yaw = start.y;
        pitch = start.x;

        cooldownTimer = 0f;

        if (cooldownSlider)
        {
            cooldownSlider.minValue = 0f;
            cooldownSlider.maxValue = 1f;
            cooldownSlider.value = 1f;
        }

        if (mainCamera) mainCamera.enabled = true;
        if (replayCamera) replayCamera.enabled = false;
    }

    void Update()
    {
        HandleAim();
        HandleShoot();
        HandleCooldown();
        HandleReplayInput();
        HandleReplayCameraExit();
    }

    void HandleAim()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        yaw += mx * mouseSensitivity;
        pitch -= my * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void HandleShoot()
    {
        if (cooldownTimer > 0f)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);

            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb)
            {
                float massKg = projectileMassLbs * poundsToKg;
                rb.mass = massKg;

                // impulse = mass * velocity * power multiplier
                float impulse = massKg * muzzleVelocity * powerMultiplier;
                rb.AddForce(firePoint.forward * impulse, ForceMode.Impulse);
            }

            lastBall = ball;
            lastReplay = ball.GetComponent<SkeeBallReplay>();
            if (lastReplay)
                lastReplay.StartRecording();

            cooldownTimer = cooldownTime;

            if (cooldownSlider)
                cooldownSlider.value = 1f;

            if (fireSmoke)
                fireSmoke.Play();

            if (fireAudio)
                fireAudio.Play();
        }
    }

    void HandleCooldown()
    {
        if (cooldownTimer <= 0f)
            return;

        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer < 0f)
            cooldownTimer = 0f;

        if (cooldownSlider)
        {
            float t = cooldownTimer / cooldownTime;
            cooldownSlider.value = t;

            if (cooldownTimer == 0f)
                cooldownSlider.value = 1f;
        }
    }

    void HandleReplayInput()
    {
        if (Input.GetKeyDown(KeyCode.R) && lastReplay && lastBall)
        {
            lastReplay.StartReplay();

            if (replayCamController)
                replayCamController.SetTarget(lastBall.transform);

            if (mainCamera) mainCamera.enabled = false;
            if (replayCamera) replayCamera.enabled = true;
        }
    }

    void HandleReplayCameraExit()
    {
        if (replayCamera && replayCamera.enabled && lastReplay && !lastReplay.IsReplaying)
        {
            if (mainCamera) mainCamera.enabled = true;
            replayCamera.enabled = false;
        }
    }
}
