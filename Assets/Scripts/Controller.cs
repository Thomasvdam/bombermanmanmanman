using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class Controller : MonoBehaviour {
	public XboxController playerNumber = XboxController.First;
	private Player player;

	private float x;
	private float y;

	private float aimX;
	private float aimY;

	private bool armed = false;

	void Start () {
		player = gameObject.GetComponent<Player> ();
	}

	// Update is called once per frame
	void Update () {
		checkAxes ();
		checkFireButton ();
		checkAbilityButton ();
	}

	private void checkAxes() {
		y = XCI.GetAxisRaw(XboxAxis.LeftStickY, playerNumber);
		x = XCI.GetAxisRaw(XboxAxis.LeftStickX, playerNumber);

		aimX = XCI.GetAxisRaw (XboxAxis.RightStickX, playerNumber);
		aimY = XCI.GetAxisRaw (XboxAxis.RightStickY, playerNumber);

		player.Move (x, y);
		player.Aim (aimX, aimY);
	}

	private void checkFireButton() {
		if (XCI.GetAxis(XboxAxis.RightTrigger, playerNumber) >= 0.8 && !armed) {
			player.ArmBomb ();
			armed = true;
		} else if (XCI.GetAxis(XboxAxis.RightTrigger, playerNumber) < 0.8 && armed) {
			player.ThrowBomb (aimX, aimY);
			armed = false;
		}
	}

	private void checkAbilityButton() {
		if (XCI.GetButtonDown(XboxButton.B, playerNumber)) {
			Debug.Log ("Abilitybutton down");
		}
	}
}