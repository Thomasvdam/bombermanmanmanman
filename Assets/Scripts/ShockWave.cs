using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

	public float maxRadius = 3;
	public float explosionFactor = 2;
	private float blastPowerFactor = 1200f;
	public float blastScale = 1;
	private float blastCap = 3;

	public Sprite[] explosion;
	public float animationDuration = 0.1f;
	private SpriteRenderer myRenderer;

	float minRadius = 0.01f;
	float currentTime;

	CircleCollider2D circleCollider;

	void Start () {
		circleCollider = GetComponent<CircleCollider2D> ();
		circleCollider.radius = minRadius;
		myRenderer = GetComponent<SpriteRenderer> ();
		currentTime = Time.time;
		StartCoroutine(StartAnimation(animationDuration));
	}

	void Update () {
		//the lerp time factor
		float t = Time.time - currentTime;
		//increase the radius of the collider
		circleCollider.radius = Mathf.Lerp (minRadius, maxRadius, t * explosionFactor);
		//if we reach max radius destroy the gameobject
		if (circleCollider.radius >= maxRadius) Destroy(this.gameObject);
	}

	void OnTriggerEnter2D (Collider2D other) {
		Player otherPlayer = other.GetComponent<Player> ();
		//if the shockwave blast hits the player
		if (other.tag == "Player" && !otherPlayer.IsGracePeriodActive()) {
			Vector2 dir = (other.transform.position - transform.position).normalized;

			float blastPower = Mathf.Clamp(maxRadius / circleCollider.radius, 0, blastCap);
			//get player rigidbody and add force inverse to the distance from the center
			other.GetComponent<Rigidbody2D>().AddForce (blastPowerFactor * dir * blastPower);

			otherPlayer.Vibrate (blastPower / 2f, blastPower / 2f, blastPower / 6f);
		}
	}

	IEnumerator StartAnimation(float timer) {
		yield return new WaitForSeconds (0.1f);
		foreach (Sprite x in explosion) {
			myRenderer.sprite = x;
			yield return new WaitForSeconds ((timer / explosion.Length));
		}

		myRenderer.sprite = null;
	}
}
