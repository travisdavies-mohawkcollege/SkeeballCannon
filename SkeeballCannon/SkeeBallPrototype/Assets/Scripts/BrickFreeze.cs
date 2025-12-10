using UnityEngine;

public class BrickFreeze : MonoBehaviour
{
    Rigidbody rb;
    bool activated = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll; // freeze until triggered
    }

    public void Unfreeze()
    {
        if (activated)
            return;

        activated = true;
        rb.constraints = RigidbodyConstraints.None;
    }
}
