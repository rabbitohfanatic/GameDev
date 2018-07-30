using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour {

    public Rigidbody rb;

    public float xVel = 100f;
    public float zVel = 4000f;
    bool moveRight;
    bool moveLeft;


    // Use this for initialization
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)){
            moveRight = true;
        } else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)){
            moveLeft = true;
		} else {

            moveRight = false;
            moveLeft = false;
		}
    }
    void FixedUpdate()
    {
        rb.AddForce(0, 0, zVel * Time.deltaTime);

        if (moveRight)
        {
			rb.AddForce(xVel * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            Debug.Log("move right");
        }

        if (moveLeft)
        {
			rb.AddForce(-xVel * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            Debug.Log("move left");
        }
    }
}
