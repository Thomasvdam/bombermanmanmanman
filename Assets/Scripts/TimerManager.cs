using UnityEngine;
using System.Collections;
using System;

public class TimerManager : MonoBehaviour {

	public UnityEngine.UI.Text timerText; //Displays the text in our UI
	private float time; //we use this variable to keep track of the time during update steps
	private bool startTimer = false; //starts/ stops the timer and allows/ blocks countdown
	private bool secondSoundActivated = false; //whether we have started playing the sound for seconds counting down

	public event EventHandler onSecondSound; //handles that we want to play the sound for a second counting down
	public event EventHandler onTimerEndedSound; //handles that we want to play the sound for the timer ending

	//containing event variables (the amount of seconds we want to count down from, 
	//the object we want to notify once we've stopped counting down)
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
		StartAreaScript startAreaScript = startArea.GetComponent<StartAreaScript> ();
		startAreaScript.startTimerHandler += this.handleStartTimerEvent;
		startAreaScript.abortTimerHandler += this.handleAbortTimerEvent;
	}

	public void handleStartTimerEvent(object sender, System.EventArgs args) {
		mArgs = (TimerArgs) args;
		startTimer = true;
		time = mArgs.countdownTime;
	}

	public void handleAbortTimerEvent(object sender, System.EventArgs args) {
		startTimer = false;
		reset ();
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
		if (!secondSoundActivated && time < 10f) { //TODO do we still want 10 as a limit?
			secondSoundActivated = true;
			InvokeRepeating("onSecondSoundEvent", 1f, 1f);
		}
	}

	//Checks if the time has elapsed. TRUE: resets variables, plays timer ending sound and notifies subscribers that the timer has ended
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
