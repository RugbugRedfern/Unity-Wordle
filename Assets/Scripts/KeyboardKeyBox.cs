using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An individual key on the in-game keyboard, which will turn gray when it's found missing
/// </summary>
public class KeyboardKeyBox : MonoBehaviour
{
	[SerializeField] TMP_Text text;
	[SerializeField] Image background;

	char key;

	/// <summary>
	/// Construct the KeyboardKeyBox
	/// </summary>
	/// <param name="key">The key for the key box to represent on the keyboard</param>
	public void Initialize(char key)
	{
		this.key = key;
		text.text = key.ToString().ToUpper();
	}

	void OnEnable()
	{
		Row.OnSubmit += OnSubmit;
	}

	void OnDisable()
	{
		Row.OnSubmit -= OnSubmit;
	}

	/// <summary>
	/// Called when enter is pressed. Update the visuals based on the missing letters
	/// </summary>
	void OnSubmit()
	{
		if(Wordle.ConfirmedMissingLetters.Contains(key))
		{
			background.gameObject.SetActive(false);
		}
	}
}
