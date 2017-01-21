using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 1;

	private PlayerActions actions;
	private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
		actions = GetComponent<PlayerActions> ();
		rBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Move(float horizontal, float vertical) {
		Vector2 direction = new Vector2 (horizontal, vertical);

		rBody.velocity = transform.TransformDirection(direction * speed);
	}

	public void ArmBomb() {
		actions.ArmBomb ();
	}

	public void ThrowBomb() {
		actions.ThrowBomb (rBody.velocity.normalized);
	}
}
