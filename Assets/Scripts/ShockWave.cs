using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

	public float maxRadius;
	public float explosionFactor;
	public float blastPowerFactor;
	public float blastScale;
	public float blastCap;

	float minRadius = 0.001f;
	float currentTime;

	Transform radius;

	void Start () {
		radius = this.transform;
		radius.localScale = new Vector2(1, 1) * minRadius;
		currentTime = Time.time;
	}

	void Update () {
		//the lerp time factor
		float t = 1 + Mathf.Log (Time.time - currentTime);
		//increase the radius of the collider
		radius.localScale = new Vector2(1, 1) * Mathf.Lerp (minRadius, maxRadius, t * explosionFactor);
		//if we reach max radius destroy the gameobject
		if (radius.localScale.x >= maxRadius) Destroy(this.gameObject);
	}

	void OnTriggerEnter2D (Collider2D other) {
		//if the shockwave blast hits the player
		if (other.tag == "Player") {
			Vector2 dir = (other.transform.position - transform.position).normalized;

			float blastPower = Mathf.Clamp(-Mathf.Log (Mathf.Pow (radius.localScale.x, blastScale) / maxRadius), 0, blastCap);

			//get player rigidbody and add force inverse to the distance from the center
			other.GetComponent<Rigidbody2D>().AddForce (blastPowerFactor * dir * blastPower);

		}
	}
}
