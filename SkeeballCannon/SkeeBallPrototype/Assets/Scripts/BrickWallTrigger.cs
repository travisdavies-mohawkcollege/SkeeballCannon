using UnityEngine;

public class BrickWallTrigger : MonoBehaviour
{
    public BrickFreeze[] bricks;  // drag all bricks in here

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("SkeeBall"))
            return;

        Debug.Log("SkeeBall entered wall trigger — unfreezing bricks!");

        foreach (BrickFreeze brick in bricks)
        {
            if (brick != null)
                brick.Unfreeze();
        }
    }
}
