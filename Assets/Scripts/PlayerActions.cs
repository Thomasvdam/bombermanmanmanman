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
	public GameObject powerBarPrefab;
	private GameObject powerBarObj;
	private PowerBar powerBar;

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

		newBomb.GetComponent<BombBehaviour> ().onBombDespawns += this.DestroyPowerBar;
		powerBarObj = Instantiate (powerBarPrefab, gameObject.transform.position + Vector3.down, gameObject.transform.rotation, gameObject.transform) as GameObject;
		powerBar = powerBarObj.GetComponent<PowerBar> ();

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

		this.DestroyPowerBar ();
		newBomb.GetComponent<BombBehaviour> ().onBombDespawns -= this.DestroyPowerBar;
		newBomb = null;
	}

	public void setBombAim(Vector2 dir) {
		if (!newBomb) {
			return;
		}

		Vector2 normalizedDir = dir.normalized;

		if (normalizedDir.magnitude <= 0) {
			if (oldBombAim.magnitude > 0) {
				normalizedDir = oldBombAim;
			} else {
				normalizedDir = new Vector2 (0, 1);
			}
		}

		if (powerBar != null) {
			powerBar.UpdateBar(dir);
		}

		newBomb.transform.position = (Vector2)transform.position + normalizedDir * 1f;
		oldBombAim = normalizedDir;
	}

	private void DestroyPowerBar() {
		Destroy (powerBarObj);
		powerBar = null;
	}
}
