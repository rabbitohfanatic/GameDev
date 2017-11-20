using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PrizeBlock : MonoBehaviour {

	private SpriteRenderer sr;

	public AnimationCurve anim;

	public int coinsInBlock = 5;

	public Sprite explodedBlock;

	public float secBeforeSpriteChange = 0.2f;

	void Awake() {
		sr = GetComponent<SpriteRenderer> ();
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.contacts [0].point.y < transform.position.y) {

			if (coinsInBlock > 0) {

				if (coinsInBlock == 1) {
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.rockSmash);

					sr.sprite = explodedBlock;
					DestroyObject (gameObject, secBeforeSpriteChange);

					IncreaseTextUIScore ();
				} else {

					StartCoroutine (RunAnimation ());
						
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.getCoin);

					IncreaseTextUIScore ();

					--coinsInBlock;
				}
			}
				
		}
	}

	IEnumerator RunAnimation(){
		Vector2 startPos = transform.position;

		for(float x = 0; x < anim.keys[anim.length-1].time; x+= Time.deltaTime){
			transform.position = new Vector2(startPos.x,
				startPos.y + anim.Evaluate(x));

			yield return null;
		}
	}

	void IncreaseTextUIScore(){
		var textUIComp = GameObject.Find ("Score").GetComponent<Text> ();
		int score = int.Parse (textUIComp.text);
		score += 10;
		textUIComp.text = score.ToString ();
	}
}
