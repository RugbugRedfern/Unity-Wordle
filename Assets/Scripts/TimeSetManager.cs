using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// manages the time to play being set in the main menu
public class TimeSetManager : MonoBehaviour
{
	public static float Time = 300f;
	[SerializeField] TMP_Text timeText;

	public void SetTimer(float value)
	{
		Time = value;
		timeText.text = Time.ToString("0") + " Seconds";
	}
}
