using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour {

	public float speed = 5;

	private Rigidbody2D rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		float horzMove = Input.GetAxisRaw ("Horizontal");

		rigidBody.velocity = new Vector2 (horzMove, 0) * speed;
	}
}

