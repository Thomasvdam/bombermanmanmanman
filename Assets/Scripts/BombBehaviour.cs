using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour, IFallable {

	public GameObject shockwave;

	public float waitTillExplode = 3;
	public float fallingSpeed = 1;
	public float fallingDuration = 2;
    public float thrownScaleFactor = 3;

	private bool isFalling = false;
	private bool isThrown = false;
    private float travelTime;
    private float armedTime;
    private float thrownTime;
    private float ratioThrownTime;
    private float scale;
    private Vector3 initialScale;

	private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
		// Begin Timer for explosion
		StartCoroutine(Explode(waitTillExplode));
		rBody = GetComponent<Rigidbody2D> ();
        initialScale = transform.localScale;
	}

	void Update () {
		if (isFalling) {
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime * fallingSpeed);
		}
	}

    void FixedUpdate () {
        if (!isFalling && isThrown) {
            ratioThrownTime = (Time.time - thrownTime) / travelTime; 
            scale = -(Mathf.Pow(ratioThrownTime * 2.0f, 2.0f)) + 2 * (ratioThrownTime * 2.0f);
            transform.localScale = new Vector3(initialScale.x + scale * thrownScaleFactor, initialScale.y + scale * thrownScaleFactor, initialScale.z + scale * thrownScaleFactor);
        }
    }

	IEnumerator Explode (float waitTillExplode){
		yield return new WaitForSeconds (waitTillExplode);
		//Instantiate shockwave when time is over
		if (gameObject) {
			Instantiate (shockwave, transform.position, Quaternion.identity);
			Destroy (this.gameObject);	
		}
	}

    public void Armed () {
        armedTime = Time.time;
    }

    public void Thrown () {
        isThrown = true;
        thrownTime = Time.time;
        travelTime = waitTillExplode - (Time.time - armedTime);
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
