using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manny : MonoBehaviour {

	public float speed = 5.0f;

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator animator;

	public bool facingRight = true;

	public float jumpSpeed = 5f;

	bool isJumping = false;

	private float rayCastLength = 0.005f;

	private float width;
	private float height;

	private float jumpButtonPressTime;

	private float maxJumpTime = 0.2f;

	public float wallJumpY = 10f;

	void FixedUpdate(){
		float horzMove = Input.GetAxisRaw ("Horizontal");
		Vector2 vect = rb.velocity;
		rb.velocity = new Vector2 (horzMove * speed, vect.y);

		if (IsWallOnLeftOrRight () && !IsOnGround () && horzMove == 1) {
			rb.velocity = new Vector2 (-GetWallDirection () * speed * -0.75f, wallJumpY);
		}
		animator.SetFloat ("Speed", Mathf.Abs(horzMove));

		if (horzMove < 0 && !facingRight) {
			FlipManny ();
		} else if (horzMove > 0 && facingRight){
			FlipManny (); 
		}

		float vertMove = Input.GetAxis ("Jump");

		if (IsOnGround () && isJumping == false) {
			if (vertMove > 0f) {
				isJumping = true;
				SoundManager.Instance.PlayOneShot (SoundManager.Instance.jump);
			} 
		}

		if (jumpButtonPressTime > maxJumpTime) {
			vertMove = 0f;
		}

		if (isJumping && (jumpButtonPressTime < maxJumpTime)) {
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
		}

		if (vertMove >= 1f) {
			jumpButtonPressTime += Time.deltaTime;
		} else {
			isJumping = false;
			jumpButtonPressTime = 0f;
		}
	}

	void Awake(){
		sr = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		width = GetComponent<Collider2D> ().bounds.extents.x + 0.1f;
		height = GetComponent<Collider2D> ().bounds.extents.y + 0.2f;
	}
	void FlipManny (){
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public bool IsOnGround(){
		bool groundCheck1 = Physics2D.Raycast (new Vector2 (
			                    transform.position.x,
			                    transform.position.y - height),
			                    -Vector2.up,
			                    rayCastLength);

		bool groundCheck2 = Physics2D.Raycast (new Vector2 (
			transform.position.x + (width - 0.2f),
			transform.position.y - height),
			-Vector2.up,
			rayCastLength);

		bool groundCheck3 = Physics2D.Raycast (new Vector2 (
			transform.position.x - (width - 0.2f),
			transform.position.y - height),
			-Vector2.up,
			rayCastLength);

		if(groundCheck1 || groundCheck2 || groundCheck3){
			return true;
		} else {
			return false;
		}
	}

	void OnBecomeInvisible(){
		Debug.Log ("Manny Destroyed");
		Destroy (gameObject);
	}

	public bool IsWallOnLeft(){
		return Physics2D.Raycast (new Vector2 (transform.position.x - width, transform.position.y),
			-Vector2.right, 
			rayCastLength);
	}

	public bool IsWallOnRight(){
		return Physics2D.Raycast (new Vector2 (transform.position.x + width, transform.position.y),
			-Vector2.right, 
			rayCastLength);
	}

	public bool IsWallOnLeftOrRight(){
		if (IsWallOnLeft () || IsWallOnRight ()) {
			return true;
		} else {
			return false;
		}
	}

	public int GetWallDirection(){
		if (IsWallOnLeft ()) {
			return -1;
		} else if (IsWallOnRight ()) {
			return 1;
		} else {
			return 0;
		}

	}

}
