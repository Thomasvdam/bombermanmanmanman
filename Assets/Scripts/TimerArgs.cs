using System;
using UnityEngine;

/*
 * Welcome to the TimerArgs class. This class holds the variables for the startTimerEvent.
*/
public class TimerArgs : System.EventArgs
{
	public float countdownTime; //amount of seconds to countdown from
	public event EventHandler timerEndedHandler; //handler that will be called once the timer has down counting down

	//will be called by the TimerManager when the timer has succesfully counted down
	public void onTimerEndedEvent() {
		EventHandler handler = timerEndedHandler;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
			handler = null;
		}
	}

	public TimerArgs (EventHandler timerEndedHandler, float countdownTime)
	{
		this.timerEndedHandler += timerEndedHandler;
		this.countdownTime = countdownTime;
	}

	public TimerArgs (float countdownTime)
	{
		this.countdownTime = countdownTime;
	}
}

