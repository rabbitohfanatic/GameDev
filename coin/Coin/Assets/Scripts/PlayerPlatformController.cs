using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7; 

    [Range(1, 10)]
    public float jumpVelocity;

	// Use this for initialization
	void Start () {
		
	}
	
    protected override void ComputeVelocity(){
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && grounded){
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButton("Jump")){
            if(velocity.y > 0){
                velocity.y = velocity.y * .5f;
            }
        }

        targetVelocity = move * maxSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
    }
}
