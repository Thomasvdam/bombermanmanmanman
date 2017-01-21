using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
	public GameObject projectile;

	public float fireCooldown = 1f;
	public float throwingStrenght = 300f;

	private float cooldownTimeStamp;
	private GameObject newBomb;

	public void ArmBomb() {
		if (cooldownTimeStamp >= Time.time) {
			return;
		}

		newBomb = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
		newBomb.transform.SetParent (gameObject.transform);

		this.cooldownTimeStamp = Time.time + fireCooldown;
	}

	public void ThrowBomb(Vector2 direction) {
		if (!newBomb) {
			return;
		}

		newBomb.transform.SetParent (null);
		newBomb.GetComponent<Rigidbody2D> ().AddForce (direction * throwingStrenght);
		newBomb = null;
	}
}
