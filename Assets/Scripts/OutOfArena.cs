using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfArena : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Arena") {
			GetComponent<IFallable> ().Fall();
		}
	}
}
