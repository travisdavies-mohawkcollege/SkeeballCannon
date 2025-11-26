using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Wind winds;
    public GameObject windObject;
    public Rigidbody rb;
    private bool inWind;
    // Start is called before the first frame update
    void Start()
    {
        //Find wind script.
        windObject = GameObject.Find("WindPoint");
        winds = windObject.GetComponent<Wind>();
    }

    void LateUpdate()
    {
        //Unity applies physics in LateUpdate, so I do as well.
        if(inWind)
        {
            rb.AddForce(winds.windForceVector);
            //Debug.Log("applying wind to ball!");
        }
    }


    //Manage being in wind. Was having issues with OnTriggerStay but may revisit.
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Wind"))
        {
            inWind = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.CompareTag("Wind"))
        {
            inWind = false;
        }
    }
}
