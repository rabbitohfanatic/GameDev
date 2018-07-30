using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlledMovement : MonoBehaviour {

	public Rigidbody rb;

    public float xVel = 100f;
    public float zVel = 4000f;

    bool moveRight;
    bool moveLeft;
	bool moveForward;
	bool moveBack;

	Vector3 xAxis = new Vector3(0, 0, 0);
	Vector3 zAxis = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		moveRight = Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow);
        
		moveLeft = Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow);
        
		moveBack = Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow);
        
		moveForward = Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow);

        
	}

	private void FixedUpdate()
	{
		if (moveRight)
        {
			Debug.Log("Right");
			if (rb.velocity.x < 20)
			{
				rb.AddForce(xVel * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
				Debug.Log(rb.velocity);
			}
		    else {
                
				rb.velocity = new Vector3(20, rb.velocity.y, rb.velocity.z);
				Debug.Log(rb.velocity);
			}
            
        }

        if (moveLeft)
        {
			Debug.Log("Left");
			if (rb.velocity.x > -20)
            {
                rb.AddForce(- xVel * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                Debug.Log(rb.velocity);
            }
            else
            {

                rb.velocity = new Vector3(-20, rb.velocity.y, rb.velocity.z);
                Debug.Log(rb.velocity);
            }
        }
        
		if (moveBack)
        {
			Debug.Log("Back");
			if (rb.velocity.z > -20)
            {
				rb.AddForce(0, 0, -zVel * Time.deltaTime, ForceMode.VelocityChange);
                Debug.Log(rb.velocity);
            }
            else
            {

				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -20);
                Debug.Log(rb.velocity);
            }
        }

		if (moveForward)
		{
			Debug.Log("Forward");
			if (rb.velocity.z < 20)
			{
				rb.AddForce(0, 0, zVel * Time.deltaTime, ForceMode.VelocityChange);
				Debug.Log(rb.velocity);
			}
			else
			{

				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 20);
				Debug.Log(rb.velocity);
			}
		}
	}
}
