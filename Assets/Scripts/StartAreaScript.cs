using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAreaScript : MonoBehaviour {

	private const int AMOUNT_PLAYERS_IN_AREA_NEEDED = 4;
	private const string TAG_PLAYER = "Player";

	private int mPlayersInArea = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D co) {
		Debug.Log ("onTriggerEnter2D");
		if (co.tag.Equals (TAG_PLAYER)) {
			mPlayersInArea++;
			if (mPlayersInArea == AMOUNT_PLAYERS_IN_AREA_NEEDED) {
				StartTimer ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D co) {
		Debug.Log ("onTriggerExit2D");
		if (co.tag.Equals (TAG_PLAYER)) {
			mPlayersInArea--;
		}
	}

	private void StartTimer() {
	}
}
