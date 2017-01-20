﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

	public GameObject shockwave;

	public float waitTillExplode;

	// Use this for initialization
	void Start () {
		// Begin Timer for explosion
		StartCoroutine(Explode(waitTillExplode));
	}

	IEnumerator Explode (float waitTillExplode){
		yield return new WaitForSeconds (waitTillExplode);
		//Instantiate shockwave when time is over
		Instantiate (shockwave, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}

}
