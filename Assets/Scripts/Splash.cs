using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

	public Sprite[] splash;
	public float animationDuration = 0.1f;
	private SpriteRenderer myRenderer;

	public event System.EventHandler onPlonsHandler; //handles that we want to play the sound for falling (plonsing)

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer> ();
		GameManager.getInstance ().RegisterSplash (this);
		StartCoroutine(StartAnimation(animationDuration));
	}

	public void onPlonsEvent() {
		System.EventHandler handler = onPlonsHandler;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
		}
	}
	
	IEnumerator StartAnimation(float timer) {
		yield return new WaitForSeconds (0.1f);
		onPlonsEvent ();

		foreach (Sprite x in splash) {
			myRenderer.sprite = x;
			yield return new WaitForSeconds ((timer / splash.Length));
		}

		Destroy (gameObject);
	}
}
