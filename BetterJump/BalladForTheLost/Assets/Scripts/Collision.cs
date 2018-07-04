using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	public NewPlayerMovement movement;

	private void OnCollisionEnter(UnityEngine.Collision collision)
	{
		if(collision.collider.tag == "Obstacle"){
			Debug.Log("Hit Obstacle");

			movement.enabled = false;
		}


	}

}
