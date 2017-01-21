using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class Controller : MonoBehaviour {
	public XboxController playerNumber = XboxController.First;
	private Player player;

	private float x;
	private float y;

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
		x = XCI.GetAxis(XboxAxis.LeftStickX, playerNumber);
		y = XCI.GetAxis(XboxAxis.LeftStickY, playerNumber);

		player.Move (x, y);
	}

	private void checkFireButton() {
		if (XCI.GetButtonDown(XboxButton.A, playerNumber)) {
			player.ArmBomb ();
		} else if (XCI.GetButtonUp(XboxButton.A, playerNumber)) {
			player.ThrowBomb (x, y);
		}
	}

	private void checkAbilityButton() {
		if (XCI.GetButtonDown(XboxButton.B, playerNumber)) {
			Debug.Log ("Abilitybutton down");
		}
	}
}