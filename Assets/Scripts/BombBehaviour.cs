using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour, IFallable {

	public GameObject shockwave;

	public float waitTillExplode = 3;
	public float fallingSpeed = 1;
	public float fallingDuration = 2;
    public float projectileScaleFactor = 0.5f;
    public float rotationSpeed = 3.0f;

	public delegate void EventHandler();
	public event EventHandler onBombDespawns;

	public GameObject splashPrefab;

	private bool isFalling = false;
	private bool isThrown = false;
    private float travelTime;
    private float armedTime;
    private float thrownTime;
    private float ratioThrownTime;
    private float scale;
    private Vector3 initialScale;

	private Rigidbody2D rBody;

	public event System.EventHandler onBombExplodeHandler; //handles that we want to play the sound for throwing a bomb
	public void onBombExplode() {
		System.EventHandler handler = onBombExplodeHandler;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
		}
	}

	// Use this for initialization
	void Start () {
		GameManager.getInstance().RegisterBomb(this);
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
        if (isThrown){
            ScaleProjectile();
            RotateProjectile();
        }
    }

	IEnumerator Explode (float waitTillExplode) {
		yield return new WaitForSeconds (waitTillExplode);

		//Instantiate shockwave when time is over
		if (gameObject != null && !isFalling) {
			Instantiate (shockwave, transform.position, Quaternion.identity);
			onBombExplode ();
			Destroy (this.gameObject);
			if (onBombDespawns != null) {
				onBombDespawns ();
			}
		} else if (gameObject != null) {
			Instantiate (splashPrefab, gameObject.transform.position, Quaternion.identity);

			Destroy (this.gameObject);
			if (onBombDespawns != null) {
				onBombDespawns ();
			}
		}
	}

    private void ScaleProjectile () {
        ratioThrownTime = (Time.time - thrownTime) / travelTime;
        scale = -(Mathf.Pow(ratioThrownTime * 2.0f, 2.0f)) + 2 * (ratioThrownTime * 2.0f);
        transform.localScale = new Vector3(initialScale.x + scale * projectileScaleFactor, initialScale.y + scale * projectileScaleFactor, initialScale.z + scale * projectileScaleFactor);
    }

    private void RotateProjectile () {
        rBody.MoveRotation(rBody.rotation + rotationSpeed + Time.fixedDeltaTime);
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
	}
}
