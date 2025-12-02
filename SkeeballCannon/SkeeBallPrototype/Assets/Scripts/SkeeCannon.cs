using UnityEngine;
using UnityEngine.UI;

public class SkeeCannon : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject ballPrefab;
    public Transform firePoint;
    public float shootForce = 15f;

    [Header("Cooldown Settings")]
    public float cooldownTime = 2f;
    private float cooldownTimer = 0f;
    public Slider cooldownSlider;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 3f;
    public float minPitch = -30f;
    public float maxPitch = 45f;

    float yaw;
    float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 start = transform.eulerAngles;
        yaw = start.y;
        pitch = start.x;

        if (cooldownSlider != null)
        {
            cooldownSlider.maxValue = cooldownTime;
            cooldownSlider.value = cooldownTime;
        }
    }

    void Update()
    {
        HandleAim();
        HandleCooldown();
        HandleShoot();
    }

    void HandleAim()
    {
        // get mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // update yaw and pitch
        yaw += mouseX * mouseSensitivity;
        pitch -= mouseY * mouseSensitivity;

        // clamp vertical rotation
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // apply rotation
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void HandleCooldown()
    {
        // timer counts upward until it reaches cooldownTime
        if (cooldownTimer < cooldownTime)
            cooldownTimer += Time.deltaTime;

        // update slider display
        if (cooldownSlider != null)
            cooldownSlider.value = cooldownTimer;
    }

    void HandleShoot()
    {
        // cannot shoot if timer is not full
        if (cooldownTimer < cooldownTime)
            return;

        // shoot on click
        if (Input.GetMouseButtonDown(0))
        {
            GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);

            // reset cooldown
            cooldownTimer = 0f;

            if (cooldownSlider != null)
                cooldownSlider.value = 0f;
        }
    }
}
