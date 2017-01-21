using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private static GameManager mManager = null;
	private AudioManager audioManager;

	public float waitUntilFinish;

	private PauseManager pauseManager;

	public int numberOfPlayers = 4;
	public int numberOfLives = 5;
	private List<int> lives = new List<int>();

	public GameObject playerPrefab1;
	public GameObject playerPrefab2;
	public GameObject playerPrefab3;
	public GameObject playerPrefab4;

	public GameObject spawnLocation1;
	public GameObject spawnLocation2;
	public GameObject spawnLocation3;
	public GameObject spawnLocation4;

	public void onPlayerDeathEvent(object sender, int id) {
		if (Constants.isStartedGame) {
			lives [id - 1]--;
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

		StartGame ();
	}

	IEnumerator Finish (float waitUntilFinish){
		yield return new WaitForSeconds (waitUntilFinish);
		pauseManager.OnPause ();
	}

	void StartGame() {
		// Begin Timer for Finish
		StartCoroutine(Finish(waitUntilFinish));

		for (int i = 0; i < numberOfPlayers; i++) {
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
			break;
		case 2:
			prefab = playerPrefab2;
			location = spawnLocation2;
			break;
		case 3:
			prefab = playerPrefab3;
			location = spawnLocation3;
			break;
		case 4:
			prefab = playerPrefab4;
			location = spawnLocation4;
			break;
		default:
			prefab = playerPrefab1;
			location = spawnLocation1;
			break;
		}

		GameObject playerObject = Instantiate (prefab, location.transform.position, location.transform.rotation);
		playerObject.GetComponent<Player> ().onPlayerDeath += this.onPlayerDeathEvent;
		audioManager = GetComponent<AudioManager> ();
		playerObject.GetComponent<Player> ().onFootStepHandler += audioManager.handleOnFootStepEvent;
		playerObject.GetComponent<Player> ().onPlonsHandler += audioManager.handleOnPlonsEvent;
	}

}
