using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

	public float finalRadius;
	public float explosionFactor;
	public float blastPowerFactor;

	float minRadius = 0.001f;
	float currentTime;

	CircleCollider2D circleCollider;
	Rigidbody2D playerRb;

	void Start () {
		circleCollider = GetComponent<CircleCollider2D> ();
		//make the collider really small if it is not already
		circleCollider.radius = minRadius;
		currentTime = Time.time;
	}

	void Update () {
		//the lerp time factor
		float t = 1 + Mathf.Log (Time.time - currentTime);
		//increase the radius of the collider
		circleCollider.radius = Mathf.Lerp (minRadius, finalRadius, t * explosionFactor);
		//if we reach max radius destroy the gameobject
		if (circleCollider.radius >= finalRadius) Destroy(this.gameObject);
	}

	void OnTriggerEnter2D (Collider2D triggerCollider) {

		//if the shockwave blast hits the player
		if (triggerCollider.tag == "Player") {

			//get distance and the direction away from the shockwave center 
			float distanceFromPlayer = (transform.position - triggerCollider.transform.position).magnitude;
			Vector2 dir = (triggerCollider.transform.position - transform.position).normalized;

			//get player rigidbody and add force inverse to the distance from the center
			playerRb = triggerCollider.GetComponent<Rigidbody2D>();
			playerRb.AddForce (blastPowerFactor * dir * 1 / distanceFromPlayer);
		}
	}
}
