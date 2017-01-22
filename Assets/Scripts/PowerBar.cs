using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour {
	public Sprite[] barLevels;

	private SpriteRenderer myRenderer;
	private float levelThreshold;

	void Start() {
		myRenderer = GetComponent<SpriteRenderer> ();
	}

	public void UpdateBar(Vector2 direction) {
		float force = Mathf.Clamp01(direction.magnitude);
		int index = Mathf.RoundToInt (force * 11);

		myRenderer.sprite = barLevels [index];
	}
}
