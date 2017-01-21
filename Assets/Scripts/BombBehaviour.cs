using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour, IFallable {

	public GameObject shockwave;

	public float waitTillExplode = 3;
	public float fallingSpeed = 1;
	public float fallingDuration = 2;

	private bool isFalling = false;

	private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
		// Begin Timer for explosion
		StartCoroutine(Explode(waitTillExplode));
		rBody = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (isFalling) {
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime * fallingSpeed);
		}
	}

	IEnumerator Explode (float waitTillExplode){
		yield return new WaitForSeconds (waitTillExplode);
		//Instantiate shockwave when time is over
		if (gameObject) {
			Instantiate (shockwave, transform.position, Quaternion.identity);
			Debug.Log ("STEVEN");
			Destroy (this.gameObject);	
		}
	}

	public void Fall () {
		isFalling = true;
		rBody.velocity = rBody.velocity / 2;
		rBody.drag = 1;

		StartCoroutine(Kill(fallingDuration));
	}

	IEnumerator Kill (float timer){
		yield return new WaitForSeconds (timer);
		Destroy (this.gameObject);
	}
}
