﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

	public XboxController xboxController;
	private static GameManager mManager = null;
	private AudioManager audioManager;

	public float waitUntilFinish;

	private PauseManager pauseManager;

	public int numberOfPlayers = 4;
	public int numberOfLives = 5;
	private List<int> lives = new List<int>();
	public List<UnityEngine.UI.Text> liveTexts;

	public GameObject playerPrefab1;
	public GameObject playerPrefab2;
	public GameObject playerPrefab3;
	public GameObject playerPrefab4;

	public GameObject spawnLocation1;
	public GameObject spawnLocation2;
	public GameObject spawnLocation3;
	public GameObject spawnLocation4;

	private GameObject player1;
	private GameObject player2;
	private GameObject player3;
	private GameObject player4;

	public void onPlayerDeathEvent(object sender, int id) {
		if (Constants.isStartedGame) {
			lives [id - 1]--;
			liveTexts [id - 1].text = lives [id - 1].ToString();
			if (lives [id - 1] < 0) {
				return;
			}		
		}

		SpawnPlayer (id);
	}

	public static GameManager getInstance() {
		if (mManager == null) {
			mManager = new GameManager();
		}
		return mManager;
	}

	// Use this for initialization
	void Start () {
		pauseManager = this.GetComponent<PauseManager>();
		audioManager = GetComponent<AudioManager> ();
		foreach (UnityEngine.UI.Text textfield in liveTexts) {
			textfield.text = numberOfLives.ToString();
		}
		StartGame ();
	}

	IEnumerator Finish (float waitUntilFinish){
		yield return new WaitForSeconds (waitUntilFinish);
		pauseManager.OnPause ();
	}

	void StartGame() {
		// Begin Timer for Finish
		StartCoroutine(Finish(waitUntilFinish));

		for (int i = 0; i < XCI.GetNumPluggedCtrlrs(); i++) {
			SpawnPlayer (i + 1);
			lives.Add (numberOfLives);
		}
	}

	void SpawnPlayer(int id) {
		GameObject prefab;
		GameObject location;

		switch (id) {
		case 1:
			prefab = playerPrefab1;
			location = spawnLocation1;
			player1 = instantiatePlayer (prefab, location);
			break;
		case 2:
			prefab = playerPrefab2;
			location = spawnLocation2;
			player2 = instantiatePlayer (prefab, location);
			break;
		case 3:
			prefab = playerPrefab3;
			location = spawnLocation3;
			player3 = instantiatePlayer (prefab, location);
			break;
		case 4:
			prefab = playerPrefab4;
			location = spawnLocation4;
			player4 = instantiatePlayer (prefab, location);
			break;
		default:
			prefab = playerPrefab1;
			location = spawnLocation1;
			player1 = instantiatePlayer (prefab, location);
			break;
		}
	}

	//instantiates a player object, sets the listeners for events and return the gameobject
	private GameObject instantiatePlayer(GameObject prefab, GameObject location) {
		GameObject playerObject = Instantiate (prefab, location.transform.position, location.transform.rotation);
		Player player = playerObject.GetComponent<Player> ();
		player.onPlayerDeath += this.onPlayerDeathEvent;
		player.onFootStepHandler += audioManager.handleOnFootStepEvent;
		player.onPlonsHandler += audioManager.handleOnPlonsEvent;
		PlayerActions playerActions = playerObject.GetComponent<PlayerActions> ();
		playerActions.onArmBombHandler += audioManager.handleOnArmBombEvent;
		playerActions.onThrowBombHandler += audioManager.handleOnThrowBombEvent;
		return playerObject;
	}

}
