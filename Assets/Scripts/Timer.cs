using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// handles the countdown timer for the game
public class Timer : MonoBehaviour
{
	public static Timer Instance;
	public static Action OnTimeExpired;

	[SerializeField] TMP_Text timerText;
	[SerializeField] Image fillImage;

	float elapsedTime;
	bool paused;
	float time;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		time = TimeSetManager.Time;
	}

	void Update()
	{
		if(paused)
			return;

		// increment the elapsed time
		elapsedTime += Time.deltaTime;

		float timeLeft = time - elapsedTime;

		// update the UI
		fillImage.fillAmount = 1 - (elapsedTime / time);
		timerText.text = Mathf.Floor(timeLeft).ToString("0");

		// if the time is below zero, then let everyone subscribed to
		// the callback know
		if(timeLeft < 0)
		{
			OnTimeExpired?.Invoke();
			enabled = false;
		}
	}

	public void Pause()
	{
		paused = true;
	}

	public void Resume()
	{
		paused = false;
	}
}
