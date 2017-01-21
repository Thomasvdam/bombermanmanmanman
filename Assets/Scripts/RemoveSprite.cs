using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		Destroy (sprite);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
