using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	//Controller 1 to 4
	public int id = 1; //Default id
	private int numControllers;
	public string fireButton = "joystick 1 button 0";  //Default Fire button
	public string abilityButton = "joystick 1 button 1"; //Default Ability button

	/*
	 *	We can make the checkButton functions return a boolean value for wether the Input was consumed and then stop checking buttons for that frame. 
	 *	We can do queueing.
	 */

	// Update is called once per frame
	void Update () {
		checkAnyButtonPressedController (id);
		checkFireButton ();
		checkAxes ();
		checkAbilityButton ();
	}

	private void checkAxes() {
		float x = Input.GetAxisRaw ("joystick " + id + " X axis");
		float y = Input.GetAxisRaw ("joystick " + id + " Y axis");

		Debug.Log("joystick" + id + "Xaxis: " + x + " Y axis: " + y);

		if (x == 0 && y == 0) {
			return;
		}
	}

	private void checkFireButton() {
		if (Input.GetKeyDown (fireButton)) {
			Debug.Log ("Firebutton down");
		}
	}

	private void checkAbilityButton() {
		if (Input.GetKeyDown (abilityButton)) {
			Debug.Log ("Abilitybutton down");
		}
	}

	private bool checkButtonDown(string button) {
		return Input.GetKeyDown (button);
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