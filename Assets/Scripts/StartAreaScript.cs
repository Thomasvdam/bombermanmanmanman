using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartAreaScript : MonoBehaviour {

	public float countdownTime = 4f;
	private int mPlayersInArea = 0;
	private int mTotalPlayers = 2;

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

	void Update() {
		GameObject[] players = GameObject.FindGameObjectsWithTag (Constants.TAG_PLAYER);
		mTotalPlayers = players.Length;
		checkStartOrAbortTimer ();
	}

	public void handleTimerEndedEvent(object sender, System.EventArgs args) {
		gameObject.SetActive (false);
		//gamemanager.startgame
	}

	void OnTriggerEnter2D(Collider2D co) {
		if (co.tag.Equals (Constants.TAG_PLAYER)) {
			mPlayersInArea++;
			checkStartOrAbortTimer ();
		}
	}

	void OnTriggerExit2D(Collider2D co) {
		if (co.tag.Equals (Constants.TAG_PLAYER)) {
			mPlayersInArea--;
			checkStartOrAbortTimer ();
		}
	}

	private void checkStartOrAbortTimer() {
		if (mPlayersInArea == mTotalPlayers) {
			onStartTimerEvent ();
		} else {
			onAbortTimerEvent ();
		}
	}
}
