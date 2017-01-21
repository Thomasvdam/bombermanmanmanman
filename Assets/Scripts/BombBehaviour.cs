using System.Collections;
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

	void Update () {
		if (!transform.parent) {
			return;
		}

		transform.position = transform.parent.position + transform.parent.up * 1f; 
	}

	IEnumerator Explode (float waitTillExplode){
		yield return new WaitForSeconds (waitTillExplode);
		//Instantiate shockwave when time is over
		Instantiate (shockwave, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}

}
