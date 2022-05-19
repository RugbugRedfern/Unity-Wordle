using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// handles the screen that displays at the end of the game
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

	void OnTimeExpired()
	{
		gameScreen.SetActive(false);
		endScreen.SetActive(true);
		InputManager.OnSubmit += OnSubmit;
	}

	void OnSubmit()
	{
		InputManager.OnSubmit -= OnSubmit;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
