﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XInputDotNetPure; // Required in C#

public class Player : MonoBehaviour, IFallable {

	public int playerId = 1;
	private float speed = 3;
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
	public event System.EventHandler onFootStepHandler; //handles that we want to play the sound for a footstep
	public event System.EventHandler onPlonsHandler; //handles that we want to play the sound for falling (plonsing)

	private PlayerActions playerActions;

	public void onFootStepEvent() {
		System.EventHandler handler = onFootStepHandler;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
		}
	}

	public void onPlonsEvent() {
		System.EventHandler handler = onPlonsHandler;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
		}
	}

	// Use this for initialization
	void Start () {
		actions = GetComponent<PlayerActions> ();
		rBody = GetComponent<Rigidbody2D> ();
		InvokeRepeating ("showFootStep", 0f, 0.2f);

		playerActions = GetComponent<PlayerActions> ();
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
		if (isVelocityTooLow()) {
			return;
		}
		GameObject footstep = Instantiate (footstepPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		onFootStepEvent();

		if (isLeftFootstep) {
			footstep.GetComponent<SpriteRenderer> ().flipX = true;
		}

		isLeftFootstep = !isLeftFootstep;
	}

	private bool isVelocityTooLow() {
		return rBody.velocity.x < 1.5 && rBody.velocity.x > -1.5 && rBody.velocity.y < 1.5 && rBody.velocity.y > -1.5;
	}

	public void ArmBomb() {
		if (Constants.isStartedGame) {
			actions.ArmBomb ();
		}
	}

	public void ThrowBomb(float x, float y) {
		if (Constants.isStartedGame) {
			actions.ThrowBomb (new Vector2 (x, y));
		}
	}

	public void Fall () {
		onPlonsEvent ();
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

	public void Vibrate(float motorLeft, float motorRight, float duration) {
		StartCoroutine (ExecuteVibrate (motorLeft, motorRight, duration));
	}

	IEnumerator ExecuteVibrate (float strengthLeft, float strengthRight, float duration){
		GamePad.SetVibration ((XInputDotNetPure.PlayerIndex)playerId-1, strengthLeft, strengthRight);
		yield return new WaitForSeconds (duration);
		GamePad.SetVibration ((XInputDotNetPure.PlayerIndex)playerId-1, 0f, 0f);
	}
}
