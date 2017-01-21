using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	//Controller 1 to 4
	public int id = 1; //Default id
	private int numControllers;
	public int fireButtonID = 16;  //Default Fire button ID
	public int abilityButtonID = 14; //Default Ability button ID

	private Player player;

	void Start () {
		player = gameObject.GetComponent<Player> ();
	}

	/*
	 *	We can make the checkButton functions return a boolean value for wether the Input was consumed and then stop checking buttons for that frame. 
	 *	We can do queueing.
	 */

	// Update is called once per frame
	void Update () {
//		checkAnyButtonPressedController (id);
		checkFireButton ();
		checkAxes ();
		checkAbilityButton ();
	}

	private void checkAxes() {
		float x = Input.GetAxisRaw ("joystick " + id + " X axis");
		float y = Input.GetAxisRaw ("joystick " + id + " Y axis");

//		Debug.Log("joystick" + id + "Xaxis: " + x + " Y axis: " + y);

		player.Move (x, y);
	}

	private void checkFireButton() {
		if (Input.GetKeyDown ("joystick " + id + " button " + fireButtonID)) {
			player.ArmBomb ();
		} else if (Input.GetKeyUp("joystick " + id + " button " + fireButtonID)) {
			player.ThrowBomb ();
		}
	}

	private void checkAbilityButton() {
		if (Input.GetKeyDown ("joystick " + id + " button " + abilityButtonID)) {
			Debug.Log ("Abilitybutton down");
		}
	}

	public static bool checkAnyButtonPressedController(int id) {
		for (int i = 0; i<20; i++) {
			if (Input.GetKeyDown ("joystick " + id + " button " + i)) {
				Debug.Log ("joystick " + id + " button " + i);
				return true;
			}
		}
		return false;
	}
}