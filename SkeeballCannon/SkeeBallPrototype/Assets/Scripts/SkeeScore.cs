using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeeScore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SkeeBall"))
    {
      // Add score logic here
      Debug.Log("Score!");
  
    }
    }
}

