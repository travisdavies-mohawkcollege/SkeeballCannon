using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkeeCannon : MonoBehaviour
{
    [Header("Ball Physics")]
    public GameObject ballPrefab;
    public GameObject explosiveBallPrefab;
    public Transform firePoint;
    public float projectileMassLbs = 12f;
    const float poundsToKg = 0.45359237f;
    public float muzzleVelocity = 30f;
    public float powerMultiplier = 4f;

    [Header("Explosive Ball")]
    public bool shootBomb = false;
    public GameObject loadedShot;

    [Header("Cooldown")]
    public float cooldownTime = 2f;
    float cooldownTimer = 0f;
    public Slider cooldownSlider;   // 1 = ready, 0 = cooling

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
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera replayCamera;

    [Header("Shot Limit")]
    public int maxShots = 7;
    int shotsRemaining;
    public TMP_Text shotsText;      // shows remaining shots

    GameObject lastBall;
    SkeeBallReplay lastReplay;
    bool replayActive = false;

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

        if (!mainCamera)
            mainCamera = Camera.main;

        if (mainCamera) mainCamera.enabled = true;
        if (replayCamera) replayCamera.enabled = false;

        shotsRemaining = maxShots;
        UpdateShotsUI();
    }

    void Update()
    {
        AmmoSwitcher();
        HandleAim();
        HandleShoot();
        HandleCooldown();
        HandleReplayInput();
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

        if (shotsRemaining <= 0)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            shotsRemaining--;
            UpdateShotsUI();
            BallLoader();

            
            GameObject ball = Instantiate(loadedShot, firePoint.position, firePoint.rotation);

            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb)
            {
                float massKg = projectileMassLbs * poundsToKg;
                rb.mass = massKg;

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
        if (!Input.GetKeyDown(KeyCode.R))
            return;

        if (!replayActive)
        {
            if (lastReplay != null && lastBall != null)
            {
                lastReplay.StartReplay();
                replayActive = true;

                if (mainCamera) mainCamera.enabled = false;
                if (replayCamera) replayCamera.enabled = true;
            }
        }
        else
        {
            if (lastReplay != null)
                lastReplay.StopReplay();

            replayActive = false;

            if (mainCamera) mainCamera.enabled = true;
            if (replayCamera) replayCamera.enabled = false;
        }
    }

    void UpdateShotsUI()
    {
        if (shotsText)
            shotsText.text = "Shots: " + shotsRemaining;
    }

    void AmmoSwitcher()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!shootBomb)
            {
                shootBomb = true;
            }
            else
            {
                shootBomb = false;
            }
        }
    }

    void BallLoader()
    {
        if(shootBomb)
        {
            loadedShot = explosiveBallPrefab;
        }
        else
        {
            loadedShot = ballPrefab;
        }
    }
}
