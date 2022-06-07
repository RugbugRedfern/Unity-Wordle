using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Allows the player to set the play time
/// </summary>
public class TimeSetManager : MonoBehaviour
{
	public static float Time = 300f;
	[SerializeField] TMP_Text timeText;

	/// <summary>
	/// Called via a UI slider
	/// </summary>
	/// <param name="value">The value in seconds to set the play time to</param>
	public void SetTimer(float value)
	{
		Time = value;
		timeText.text = Time.ToString("0") + " Seconds";
	}
}
