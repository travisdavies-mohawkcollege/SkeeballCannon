using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallExplosion : MonoBehaviour
{
    public GameObject explosion;
    public float explosionRadius = 5f;
    public float explosionUpwards = 2f;
    public float explosionForce = 250f;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Kaboom!");
        Instantiate(explosion, transform);
        explosion.SetActive(true);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
            }
        }
        Destroy(this.gameObject);
    }
}
