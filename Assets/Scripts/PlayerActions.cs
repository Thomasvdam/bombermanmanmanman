using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerActions : MonoBehaviour {
	public GameObject projectile;

	public float fireCooldown = 1f;
	private float throwingStrength = 900f;

	private float cooldownTimeStamp;
	private GameObject newBomb;
	private Vector2 oldBombAim = new Vector2 (0, 0);

	public event EventHandler onArmBombHandler; //handles that we want to play the sound for activating a bomb
	public event EventHandler onThrowBombHandler; //handles that we want to play the sound for throwing a bomb

	public void onArmBombSoundEvent() {
		EventHandler handler = onArmBombHandler;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
		}
	}

	public void onThrowBombSound() {
		EventHandler handler = onThrowBombHandler;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
		}
	}

	public void ArmBomb() {
		if (cooldownTimeStamp >= Time.time) {
			return;
		}

		newBomb = Instantiate (projectile, gameObject.transform.position + gameObject.transform.up * 1f, gameObject.transform.rotation) as GameObject;

		newBomb.transform.SetParent (gameObject.transform);
		onArmBombSoundEvent ();

        newBomb.GetComponent<BombBehaviour>().Armed();

		this.cooldownTimeStamp = Time.time + fireCooldown;
	}

	public void ThrowBomb(Vector2 direction) {
		if (!newBomb) {
			return;
		}

        newBomb.GetComponent<BombBehaviour>().Thrown();
		newBomb.transform.SetParent (null);
		newBomb.GetComponent<Rigidbody2D> ().AddForce (direction * throwingStrength);
		if (direction.x != 0 || direction.y != 0) {
			onThrowBombSound ();
		}
		newBomb = null;
	}

	public void setBombAim(Vector2 dir) {
		if (!newBomb) {
			return;
		}

		if (dir.magnitude <= 0) {
			if (oldBombAim.magnitude > 0) {
				dir = oldBombAim;
			} else {
				dir = new Vector2 (0, 1);
			}
		}
			
		newBomb.transform.position = (Vector2)transform.position + dir * 1f;
		oldBombAim = dir;
	}
}
