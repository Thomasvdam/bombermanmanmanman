using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartAreaScript : MonoBehaviour {

	public const int AMOUNT_PLAYERS_IN_AREA_NEEDED = 1;
	public float countdownTime = 4f;
	private int mPlayersInArea = 0;

	public event EventHandler startTimerHandler;
	public event EventHandler abortTimerHandler;

	public void onStartTimerEvent() {
		EventHandler handler = startTimerHandler;
		if (handler != null) {
			handler(this, new TimerArgs(this.handleTimerEndedEvent, countdownTime));
		}
	}

	public void onAbortTimerEvent() {
		EventHandler handler = abortTimerHandler;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
		}
	}

	public void handleTimerEndedEvent(object sender, System.EventArgs args) {
		gameObject.SetActive (false);
		//gamemanager.startgame
	}

	void OnTriggerEnter2D(Collider2D co) {
		Debug.Log("onTriggerEnter2D: " + co.tag);
		if (co.tag.Equals (Constants.TAG_PLAYER)) {
			mPlayersInArea++;
			if (mPlayersInArea == AMOUNT_PLAYERS_IN_AREA_NEEDED) {
				Debug.Log ("Starting timer");
				onStartTimerEvent ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D co) {
		if (co.tag.Equals (Constants.TAG_PLAYER)) {
			mPlayersInArea--;
			if (mPlayersInArea < AMOUNT_PLAYERS_IN_AREA_NEEDED) {
				onAbortTimerEvent ();
			}
		}
	}
}
