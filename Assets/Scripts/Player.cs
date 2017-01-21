using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IFallable {

	public int playerId = 1;
	public float speed = 1;
	public float fallingSpeed = 1;
	public float fallingDuration = 2;

	public float gracePeriodDuration = 2;
	private float graceFlashes = 4;
	private bool gracePeriodActive = true;
	private SpriteRenderer myRenderer;
	private Color prevColor;
	public Color flashColor = new Color (1, 1, 1, 0.5f);

	public GameObject footstepPrefab;
	private bool isLeftFootstep;

	private PlayerActions actions;
	private Rigidbody2D rBody;
	private bool isFalling = false;

	public delegate void EventHandler(GameObject e, int id);
	public event EventHandler onPlayerDeath;

	// Use this for initialization
	void Start () {
		actions = GetComponent<PlayerActions> ();
		rBody = GetComponent<Rigidbody2D> ();
		InvokeRepeating ("showFootStep", 0f, 0.2f);

		myRenderer = GetComponent<SpriteRenderer> ();
		prevColor = myRenderer.color;

		myRenderer.color = flashColor;
		StartCoroutine(EndGracePeriod(gracePeriodDuration));
	}
	
	// Update is called once per frame
	void Update () {
		if (isFalling) {
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime * fallingSpeed);
		}
	}

	public void Move(float horizontal, float vertical) {
		if (isFalling) {
			return;
		}

		Vector2 direction = new Vector2 (horizontal, vertical);
		GetComponent<PlayerActions> ().setBombAim (direction.normalized);

		//if (direction.magnitude > 0) {
			//transform.up = direction;
		//}

		rBody.AddForce (direction * speed);
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

	public bool IsGracePeriodActive() {
		return gracePeriodActive;
	}

	IEnumerator EndGracePeriod(float timer) {
		for (int i = 0; i < graceFlashes; i++) {
			yield return new WaitForSeconds ((timer / graceFlashes) / 2);
			myRenderer.color = prevColor;
			yield return new WaitForSeconds ((timer / graceFlashes) / 2);
			myRenderer.color = flashColor;
		}

		myRenderer.color = prevColor;
		gracePeriodActive = false;
	}
}
