using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

	public float maxRadius = 40;
	public float explosionFactor = 2;
	public float blastPowerFactor = 350;
	public float blastScale = 1;
	public float blastCap = 4;

	float minRadius = 0.01f;
	float currentTime;

	Transform radius;

	void Start () {
		radius = this.transform;
		radius.localScale = new Vector2(1, 1) * minRadius;
		currentTime = Time.time;

		GetComponent<SpriteRenderer>().color = new Color (0f, 1f, 0f, 0.5f);
	}

	void Update () {
		//the lerp time factor
		float t = Time.time - currentTime;
		//increase the radius of the collider
		radius.localScale = new Vector2(1, 1) * Mathf.Lerp (minRadius, maxRadius, t * explosionFactor);
		//if we reach max radius destroy the gameobject
		if (radius.localScale.x >= maxRadius) Destroy(this.gameObject);
	}

	void OnTriggerEnter2D (Collider2D other) {
		//if the shockwave blast hits the player
		if (other.tag == "Player" && !other.GetComponent<Player> ().IsGracePeriodActive()) {
			Vector2 dir = (other.transform.position - transform.position).normalized;

			float blastPower = Mathf.Clamp(maxRadius / radius.localScale.x, 0, blastCap);

			//get player rigidbody and add force inverse to the distance from the center
			other.GetComponent<Rigidbody2D>().AddForce (blastPowerFactor * dir * blastPower);

		}
	}
}
