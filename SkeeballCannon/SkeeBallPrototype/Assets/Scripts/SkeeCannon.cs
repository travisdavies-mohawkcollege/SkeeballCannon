using UnityEngine;

public class SkeeCannon : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform firePoint;
    public float shootForce = 15f;

    public float mouseSensitivity = 3f;
    public float minPitch = -30f;   // how far down you can aim
    public float maxPitch = 45f;    // how far up you can aim

    float yaw;      // turning left/right
    float pitch;    // aiming up/down

    void Start()
    {
        // lock the mouse so we can use it to aim
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // start from the current rotation of the cannon
        Vector3 start = transform.eulerAngles;
        yaw = start.y;
        pitch = start.x;
    }

    void Update()
    {
        // read mouse movement each frame
        float mouseX = Input.GetAxis("Mouse X");  // left/right mouse movement
        float mouseY = Input.GetAxis("Mouse Y");  // up/down mouse movement

        // add mouse movement to yaw (horizontal rotation)
        yaw += mouseX * mouseSensitivity;

        // add mouse movement to pitch (vertical rotation)
        // subtract so "mouse up" makes cannon aim up
        pitch -= mouseY * mouseSensitivity;

        // stop the cannon from going too far up/down
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // apply the rotation to the whole cannon
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        // shoot when left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
        }
    }
}
