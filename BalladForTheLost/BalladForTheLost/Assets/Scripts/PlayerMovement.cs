using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;

    public float xVel = 600f;
    public float zVel = 2000f;
    bool moveRight;
    bool moveLeft;


    // Use this for initialization
    void Start()
    {

    }

    private void Update()
    {
        //adaif (Input.GetKey("d") || Input.GetKey("rightArrow")) ;
    }
    void FixedUpdate()
    {
        rb.AddForce(0, 0, zVel * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            rb.AddForce(xVel * Time.deltaTime, 0, 0);
            Debug.Log("move right");
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-xVel * Time.deltaTime, 0, 0);
            Debug.Log("move left");
        }
    }
}
