using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the screen that displays at the end of the game
/// </summary>
public class EndScreen : MonoBehaviour
{
	[SerializeField] GameObject gameScreen;
	[SerializeField] GameObject endScreen;

	void OnEnable()
	{
		Timer.OnTimeExpired += OnTimeExpired;
	}

	void OnDisable()
	{
		Timer.OnTimeExpired -= OnTimeExpired;
	}

	void Start()
	{
		endScreen.SetActive(false);
	}

	/// <summary>
	/// Hide the game and show the end screen, and prepare for the user to restart
	/// </summary>
	void OnTimeExpired()
	{
		gameScreen.SetActive(false);
		endScreen.SetActive(true);
		InputManager.OnSubmit += OnSubmit;
	}

	/// <summary>
	/// Called when the user presses enter while the end screen is open. Restarts the game
	/// </summary>
	void OnSubmit()
	{
		InputManager.OnSubmit -= OnSubmit;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
