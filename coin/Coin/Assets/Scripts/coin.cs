using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour {

	public float speed = 30;

	public float jumpSpeed = 8;

	private Rigidbody2D rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		float horzMove = Input.GetAxisRaw ("Horizontal");

		rigidBody.velocity = new Vector2 (horzMove, 0) * speed;

		if (Input.GetKeyDown (KeyCode.Space)) {
			rigidBody.AddForce (new Vector2 (0, 10), ForceMode2D.Impulse);
		}
	}
}

