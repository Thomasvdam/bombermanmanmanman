using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IFallable {

	public int playerId = 1;
	private float speed = 3;
	public float fallingSpeed = 1;
	public float fallingDuration = 2;

	public GameObject footstepPrefab;
	private bool isLeftFootstep;

	private PlayerActions actions;
	private Rigidbody2D rBody;
	private bool isFalling = false;

	public delegate void EventHandler(GameObject e, int id);
	public event EventHandler onPlayerDeath;

	private PlayerActions playerActions;

	// Use this for initialization
	void Start () {
		actions = GetComponent<PlayerActions> ();
		rBody = GetComponent<Rigidbody2D> ();
		InvokeRepeating ("showFootStep", 0f, 0.2f);

		playerActions = GetComponent<PlayerActions> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isFalling) {
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime * fallingSpeed);
		}
	}

	void FixedUpdate() {
		rBody.velocity *= 0.965f;
	}

	public void Move(float horizontal, float vertical) {
		if (isFalling) {
			return;
		}

		Vector2 direction = new Vector2 (horizontal, vertical);

		//rBody.AddForce (direction * speed);
		rBody.velocity += direction * speed / 10;
	}

	public void Aim(float horizontal, float vertical) {
		if (isFalling) {
			return;
		}

		playerActions.setBombAim (new Vector2(horizontal, vertical).normalized);
	}

	void showFootStep() {
		if (rBody.velocity.x == 0 && rBody.velocity.y == 0) {
			return;
		}
		GameObject footstep = Instantiate (footstepPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;

		if (isLeftFootstep) {
			footstep.GetComponent<SpriteRenderer> ().flipX = true;
		}

		isLeftFootstep = !isLeftFootstep;
	}

	public void ArmBomb() {
		actions.ArmBomb ();
	}

	public void ThrowBomb(float x, float y) {
		actions.ThrowBomb (new Vector2(x, y));
	}

	public void Fall () {
		isFalling = true;
		rBody.velocity = Vector2.zero;
		StartCoroutine(Kill(fallingDuration));
		CancelInvoke();
	}

	IEnumerator Kill (float timer){
		yield return new WaitForSeconds (timer);

		Destroy (gameObject);
		if (onPlayerDeath != null) {
			onPlayerDeath (gameObject, playerId);
		}
	}
}
