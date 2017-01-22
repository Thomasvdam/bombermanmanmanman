using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

	public XboxController xboxController;
	private static GameManager mManager = null;
	private AudioManager audioManager;

	public float waitUntilFinish;

	private PauseManager pauseManager;

	private int numberOfPlayers = 4;
	private int deadPlayers = 0;
	public int numberOfLives = 2;
	private List<int> lives = new List<int>();
	public List<UnityEngine.UI.Text> liveTexts;
	private bool gameFinished = false;
	public UnityEngine.UI.Text pauseText;

	public GameObject playerPrefab1;
	public GameObject playerPrefab2;
	public GameObject playerPrefab3;
	public GameObject playerPrefab4;

	public GameObject spawnLocation1;
	public GameObject spawnLocation2;
	public GameObject spawnLocation3;
	public GameObject spawnLocation4;

	public Sprite[] winnerSprites;
	public UnityEngine.UI.Image winnerImage;

	public void onPlayerDeathEvent(object sender, int id) {
		if (Constants.isStartedGame) {
			lives [id - 1]--;
			liveTexts [id - 1].text = lives [id - 1].ToString();
			if (lives [id - 1] < 0) {
				if (!gameFinished) {
					deadPlayers++;
					if (deadPlayers >= numberOfPlayers - 1) {
						FinishGame ();
					}
				}
				return;
			}		
		}

		SpawnPlayer (id);
	}

	public void FinishGame() {
		gameFinished = true;
		Constants.isStartedGame = false;
		pauseManager.OnPause ();

		int winnerId = 0;
		for (int i = 0; i < lives.Count; i++) {
			if (lives [i] >= 0) {
				winnerId = i + 1;
			}
		}

		String text = "Er is iets fout gegaan dus krijg je dit bericht";
		if (winnerId > 0) {
			text = "Winner: Player " + winnerId;
		} else {
			text = "No Winner";
		}
			
		pauseText.text = text;
		winnerImage.sprite = winnerSprites [winnerId - 1];
	}

	public static GameManager getInstance() {
		if (mManager == null) {
			Debug.Log("TROUBLE");
		}
		return mManager;
	}

	// Use this for initialization
	void Start () {
		mManager = this;

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

		numberOfPlayers = XCI.GetNumPluggedCtrlrs ();
	}

	void SpawnPlayer(int id) {
		GameObject prefab;
		GameObject location;

		switch (id) {
		case 1:
			prefab = playerPrefab1;
			location = spawnLocation1;
			instantiatePlayer (prefab, location);
			GameObject.Find ("Lives1").GetComponent<UnityEngine.UI.RawImage> ().enabled = true;
			GameObject.Find ("TextLives1").GetComponent<UnityEngine.UI.Text> ().enabled = true;
			break;
		case 2:
			prefab = playerPrefab2;
			location = spawnLocation2;
			instantiatePlayer (prefab, location);
			GameObject.Find ("Lives2").GetComponent<UnityEngine.UI.RawImage> ().enabled = true;
			GameObject.Find ("TextLives2").GetComponent<UnityEngine.UI.Text> ().enabled = true;
			break;
		case 3:
			prefab = playerPrefab3;
			location = spawnLocation3;
			instantiatePlayer (prefab, location);
			GameObject.Find ("Lives3").GetComponent<UnityEngine.UI.RawImage> ().enabled = true;
			GameObject.Find ("TextLives3").GetComponent<UnityEngine.UI.Text> ().enabled = true;
			break;
		case 4:
			prefab = playerPrefab4;
			location = spawnLocation4;
			instantiatePlayer (prefab, location);
			GameObject.Find ("Lives4").GetComponent<UnityEngine.UI.RawImage> ().enabled = true;
			GameObject.Find ("TextLives4").GetComponent<UnityEngine.UI.Text> ().enabled = true;
			break;
		default:
			prefab = playerPrefab1;
			location = spawnLocation1;
			instantiatePlayer (prefab, location);
			GameObject.Find ("Lives1").GetComponent<UnityEngine.UI.RawImage> ().enabled = true;
			GameObject.Find ("TextLives1").GetComponent<UnityEngine.UI.Text> ().enabled = true;
			break;
		}
	}

	//instantiates a player object, sets the listeners for events and return the gameobject
	private GameObject instantiatePlayer(GameObject prefab, GameObject location) {
		GameObject playerObject = Instantiate (prefab, location.transform.position, location.transform.rotation);
		Player player = playerObject.GetComponent<Player> ();
		player.onPlayerDeath += this.onPlayerDeathEvent;
		player.onFootStepHandler += audioManager.handleOnFootStepEvent;
//		player.onPlonsHandler += audioManager.handleOnPlonsEvent;
		PlayerActions playerActions = playerObject.GetComponent<PlayerActions> ();
		playerActions.onArmBombHandler += audioManager.handleOnArmBombEvent;
		playerActions.onThrowBombHandler += audioManager.handleOnThrowBombEvent;
		return playerObject;
	}

	public void RestartGame() {
		Constants.isSceneLoaded = false;
		SceneManager.LoadScene ("GameScene");
		Constants.isSceneLoaded = true;
	}

	public void RegisterSplash(Splash obj) {
		obj.onPlonsHandler += audioManager.handleOnPlonsEvent;
	}

	public void RegisterBomb(BombBehaviour bomb) {
		bomb.onBombExplodeHandler += audioManager.handleOnExplodeBombEvent;
	}

}
