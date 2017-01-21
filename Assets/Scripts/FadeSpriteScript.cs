using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSpriteScript : MonoBehaviour {

	public float minimum = 0.0f;
	public float maximum = 1f;
	public float duration = 5.0f;
	private float startTime;
	public SpriteRenderer sprite;

	void Start() {
		startTime = Time.time;
	}

	void Update() {
		float t = (Time.time - startTime) / duration;
		Color color = sprite.color;
		sprite.color = new Color(color.r,color.g,color.b,Mathf.SmoothStep(minimum, maximum, t));        
	}
}
