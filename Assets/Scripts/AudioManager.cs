using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {


	public AudioClip secondSound;
	public AudioClip timerSound;
	//public AudioClip winnerSound;
	public AudioClip plonsSound;
	public AudioClip armBombSound;
	public AudioClip throwBombSound;

	public AudioClip[] footStepSounds;
	public AudioClip[] explodeSounds;

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
		audio.PlayOneShot(secondSound, 1F);
	}
	
	public void handleOnTimerEndedSound(object sender, System.EventArgs args) {
		audio.PlayOneShot(timerSound, 1F);
	}

	public void handleOnFootStepEvent(object sender, System.EventArgs args) {
		audio.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Length)], 0.1F);
	}

	public void handleOnPlonsEvent(object sender, System.EventArgs args) {
		audio.PlayOneShot(plonsSound, 0.75F);
	}

	public void handleOnArmBombEvent(object sender, System.EventArgs args) {
		audio.PlayOneShot(armBombSound, 0.5F);
	}

	public void handleOnThrowBombEvent(object sender, System.EventArgs args) {
		audio.PlayOneShot(throwBombSound, 0.5F);
	}

	public void handleOnExplodeBombEvent(object sender, System.EventArgs args) {
		audio.PlayOneShot(explodeSounds[Random.Range(0, explodeSounds.Length)], 1F);
	}
}
