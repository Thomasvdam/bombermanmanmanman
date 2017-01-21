using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
	public GameObject projectile;

	public float fireCooldown = 1f;
	public float throwingStrength = 300f;

	private float cooldownTimeStamp;
	private GameObject newBomb;

	public void ArmBomb() {
		if (cooldownTimeStamp >= Time.time) {
			return;
		}

		newBomb = Instantiate (projectile, gameObject.transform.position + gameObject.transform.up * 1f, gameObject.transform.rotation) as GameObject;
		newBomb.transform.SetParent (gameObject.transform);

		this.cooldownTimeStamp = Time.time + fireCooldown;
	}

	public void ThrowBomb(Vector2 direction) {
		if (!newBomb) {
			return;
		}

		if (direction.magnitude > 0.9f) {
			//throw far
			throwingStrength = 650f;
		} else {
			//throw close
			throwingStrength = 300f;
		}

		newBomb.transform.SetParent (null);
		newBomb.GetComponent<Rigidbody2D> ().AddForce (direction.normalized * throwingStrength);
		newBomb = null;
	}
}
