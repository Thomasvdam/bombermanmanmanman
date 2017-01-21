using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public float waitUntilFinish;

	private PauseManager pauseManager;

	// Use this for initialization
	void Start () {
		// Begin Timer for Finish
		pauseManager = this.GetComponent<PauseManager>();

		StartCoroutine(Finish(waitUntilFinish));
	}

	IEnumerator Finish (float waitUntilFinish){
		yield return new WaitForSeconds (waitUntilFinish);

		pauseManager.OnPause ();
	}

}
