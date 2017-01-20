using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
	public GameObject projectile;

	private bool firing = false;
	private bool fired = true;
	private float firingDelay = 1f;
	private float currentTime = 1f;

	private GameObject newBomb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && currentTime > firingDelay) {
			if (!firing) {
				newBomb = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
				firing = true;
				fired = false;
			} else {
				newBomb.transform.position = this.transform.position;
			}
		} else {
			if (!fired) {
				Vector2 dir = new Vector2 (Input.GetAxis ("joystick 1 X axis"), Input.GetAxis ("joystick 1 Y axis"));
				Debug.Log (dir);
				newBomb.GetComponent<Rigidbody2D> ().AddForce (dir * 300f);
				fired = true;
				firing = false;
				currentTime = 0.0f;
			} else {
				currentTime += Time.deltaTime;
			}
		}
	}
}
