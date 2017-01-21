using System;
using UnityEngine;

public class TimerArgs : System.EventArgs
{
	public float countdownTime;
	public event EventHandler timerEndedHandler;

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

