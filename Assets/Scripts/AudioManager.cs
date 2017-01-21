﻿using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {


	public AudioClip secondSound;
	public AudioClip timerSound;
	//public AudioClip winnerSound;
	//public AudioClip fattySound;

	//public AudioClip[] followerEnteredSounds;

	AudioSource audio;
	
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		
		GameObject manager = GameObject.Find (Constants.NAME_GAME_MANAGER);
		//GameManager gameManagerScript = (GameManager) manager.GetComponent<GameManager> ();
		//gameManagerScript.onWinner += this.handleOnWinnerEvent;

		TimerManager roundManagerScript = (TimerManager) manager.GetComponent<TimerManager> ();
		roundManagerScript.onSecondSound += this.handleOnSecondSoundEvent;
		roundManagerScript.onTimerEndedSound += this.handleOnTimerEndedSound;
	}
	
	/*public void handleOnWinnerEvent(object sender, System.EventArgs args) {
		audio.PlayOneShot(winnerSound, 1F);
	}*/
	
	public void handleOnSecondSoundEvent(object sender, System.EventArgs args) {
		audio.PlayOneShot(secondSound, 0.5F);
	}
	
	public void handleOnTimerEndedSound(object sender, System.EventArgs args) {
		audio.PlayOneShot(timerSound, 1F);
	}

	/*public void handleOnFollowerEnteredBaseEvent(object sender, System.EventArgs args) {
		audio.PlayOneShot(followerEnteredSounds[Random.Range(0, followerEnteredSounds.Length)], 0.5F);
	}*/
}