using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Move(float horizontal, float vertical) {
		Vector2 direction = new Vector2 (horizontal, vertical);

		GetComponent<Rigidbody2D> ().velocity = transform.TransformDirection(direction * speed);
	}
}
