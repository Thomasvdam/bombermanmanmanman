using UnityEngine;
using System.Collections;
using System;

public class TimerManager : MonoBehaviour {

	public UnityEngine.UI.Text timerText;
	private float time;
	private bool startTimer = false;
	private bool secondSoundActivated = false;
	private StartAreaScript mStartAreaScript;

	public event EventHandler onSecondSound;
	public event EventHandler onTimerEndedSound;

	private TimerArgs mArgs;
	
	public void onSecondSoundEvent() {
		EventHandler handler = onSecondSound;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
		}
	}

	public void onTimerEndedSoundEvent() {
		EventHandler handler = onTimerEndedSound;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
		}
	}

	void Start() {
		//subscribe to Start- and Abort- Timer events
		GameObject startArea = GameObject.Find (Constants.NAME_START_AREA);
		mStartAreaScript = startArea.GetComponent<StartAreaScript> ();
		mStartAreaScript.startTimerHandler += this.handleStartTimerEvent;
		mStartAreaScript.abortTimerHandler += this.handleAbortTimerEvent;
	}

	public void handleStartTimerEvent(object sender, System.EventArgs args) {
		mArgs = (TimerArgs) args;
		startTimer = true;
		time = mArgs.countdownTime;
	}

	public void handleAbortTimerEvent(object sender, System.EventArgs args) {
		startTimer = false;
	}

	// Update is called once per frame
	void Update () {
		if (startTimer) {
			checkForSecondSound();
			timerText.gameObject.SetActive(true);
			time -= Time.deltaTime;
			Debug.Log ("update time: " + time + "s");
			timerText.text = (int)time + "s";
			checkTimePassed ();
		}
	}

	// Checks if we should play the sound for a second. Repeats this sound every second.
	private void checkForSecondSound() {
		if (!secondSoundActivated && time > 10f && !secondSoundActivated) {
			secondSoundActivated = true;
			InvokeRepeating("playSecondSound", 1f, 1f);
		}
	}

	private void playSecondSound() {
		onSecondSoundEvent ();
	}

	private void checkTimePassed() {
		if (time < 1) {
			reset();
			onTimerEndedSoundEvent();
			mArgs.onTimerEndedEvent ();
		}
	}

	//reset variables so the timer can be re-used again
	private void reset() {
		secondSoundActivated = false;
		startTimer = false;
		CancelInvoke();
		timerText.gameObject.SetActive(false);
	}
}
