using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private float randomX;
    private float randomY;
    private float randomZ;
    private Vector3 windDir;
    public Vector3 windForceVector;
    public float windForce;
    public float clampedForce;
    [SerializeField]public ParticleSystem windVFX;


    // Start is called before the first frame update
    void Start()
    {
        //Generate first winds.
        RandomWinds();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(windVFX.gameObject.transform.position, windVFX.gameObject.transform.forward * 15f, Color.red);
        if(Input.GetKeyDown(KeyCode.W))
        {
            //Make new winds when W is pressed.
            RandomWinds();
        }
    }

    void RandomWinds()
    {
        //Get random XYZ for rotation.
        randomX = Random.Range(0f, 360f);
        randomY = Random.Range(0f, 360f);
        randomZ = Random.Range(0f, 360f);
        windDir.x = randomX;
        windDir.y = randomY;
        windDir.z = randomZ;
        //Make random wind force.
        windForce = Random.Range(0f, 8f);
        //Put the force into an appropriate range for particle effect speed.
        clampedForce = (((windForce - 0) * (35-5)) / (8 - 0)) +5;
        Debug.Log("Wind Force is:" +windForce+ " Clamped Force is: " + clampedForce);
        //Access main properties of wind VFX.
        var main = windVFX.main;
        //Set the particles starting speed to be the previously altered number.
        main.startSpeed = clampedForce;
        //Rotate parent of particle system.
        transform.rotation = Quaternion.Euler(windDir);
        //Setup Vector3 that applies force in wind direction.
        windForceVector = windForce * windVFX.gameObject.transform.forward;
    }
}
