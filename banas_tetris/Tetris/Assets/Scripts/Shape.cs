using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Shape : MonoBehaviour {

	public static float speed = 1.0f;

	public float lastMoveDown = 0.0f; 

	// Use this for initialization
	void Start () {
		if (!IsInGrid ()) {
			SoundManager.Instance.PlayOneShot
			(SoundManager.Instance.gameOver);

			Invoke ("OpenGameOverScene", .5f);
		}

		InvokeRepeating ("IncreaseSpeed", 2.0f, 2.0f);
	}

	void OpenGameOverScene(){
		Destroy (gameObject);
		SceneManager.LoadScene ("game_over");
	}

	void IncreaseSpeed(){
		Shape.speed -= 0.001f;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("a")) {
			transform.position += new Vector3(-1, 0, 0);

			Debug.Log(transform.position);

			if (!IsInGrid ()) {
				transform.position += new Vector3 (1, 0, 0);
			} else {
				UpdateGameBoard ();

				SoundManager.Instance.PlayOneShot
				(SoundManager.Instance.shapeMove);
			}
		}

		if (Input.GetKeyDown ("d")) {
			transform.position += new Vector3 (1, 0, 0);

			Debug.Log (transform.position);

			if (!IsInGrid ()) {
				transform.position += new Vector3 (-1, 0, 0);
			} else {
				UpdateGameBoard ();

				SoundManager.Instance.PlayOneShot
				(SoundManager.Instance.shapeMove);
			}
		}

		if (Input.GetKeyDown ("s") || Time.time - lastMoveDown >= Shape.speed) {
			transform.position += new Vector3 (0, -1, 0);

			Debug.Log (transform.position);

			if (!IsInGrid ()) {
				transform.position += new Vector3 (0, 1, 0);

				bool rowDeleted = GameBoard.DeleteAllFullRows ();

				if (rowDeleted) {
					GameBoard.DeleteAllFullRows();

					IncreaseTextUIScore ();
				}

				enabled = false;

				FindObjectOfType<ShapeSpawner> ().SpawnShape();

				SoundManager.Instance.PlayOneShot
				(SoundManager.Instance.shapeStop);
			} else {
				UpdateGameBoard ();

				SoundManager.Instance.PlayOneShot
				(SoundManager.Instance.shapeMove);
			}

			lastMoveDown = Time.time;


		}

		if (Input.GetKeyDown ("w")) {
			transform.Rotate (0, 0, 90);

			Debug.Log (transform.position);

			if (!IsInGrid ()) {
				transform.Rotate (0, 0, -90);
			} else {
				UpdateGameBoard ();

				SoundManager.Instance.PlayOneShot
				(SoundManager.Instance.rotateSound);
			}
		}

		if (Input.GetKeyDown ("e")) {
			transform.Rotate (0, 0, -90);

			Debug.Log (transform.position);

			if (!IsInGrid ()) {
				transform.Rotate (0, 0, 90);
			} else {
				UpdateGameBoard ();
				SoundManager.Instance.PlayOneShot
				(SoundManager.Instance.rotateSound);
			}
		}
	}

	public bool IsInGrid(){
		int childCount = 0;

		foreach (Transform childBlock in transform) {
			Vector2 vect = RoundVector(childBlock.position);
			childCount++;

			Debug.Log(childCount + " " + childBlock.position);

			if (!IsInBorder (vect)) {
				return false;
			}

			if (GameBoard.gameBoard [(int)vect.x, (int)vect.y] != null &&
			    GameBoard.gameBoard [(int)vect.x, (int)vect.y].parent != transform) {
				return false;
			}
		}
		return true;
	}

	public Vector2 RoundVector(Vector2 vect){
		return new Vector2 (Mathf.Round (vect.x), Mathf.Round (vect.y));
	}

	public static bool IsInBorder(Vector2 pos){
		return ((int)pos.x >= 0 &&
			(int)pos.x <= 9 &&
			(int)pos.y >= 0);
	}

	public void UpdateGameBoard (){
		for (int y = 0; y < 20; ++y) {
			for (int x = 0; x < 10; x++) {
				if (GameBoard.gameBoard [x, y] != null &&
				   GameBoard.gameBoard [x, y].parent == transform) {
					GameBoard.gameBoard [x, y] = null;
				}
			}
		}

		foreach (Transform childBlock in transform) {
			Vector2 vect = RoundVector(childBlock.position);

			GameBoard.gameBoard [(int)vect.x, (int)vect.y] = childBlock;

			Debug.Log("Cube at : " + vect.x + " " + vect.y);
		}
	}

	void IncreaseTextUIScore(){
		var textUIComp = GameObject.Find ("Score").GetComponent<Text> ();
		int score = int.Parse (textUIComp.text);

		score++;

		textUIComp.text = score.ToString();
	}
}