using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An individual key on the in-game keyboard, which will update its color according to the letter states on the wordle board
/// </summary>
public class KeyboardKeyBox : MonoBehaviour
{
	[SerializeField] TMP_Text text;
	[SerializeField] Image background;
	[SerializeField] Style style;

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
	/// Called when enter is pressed. Update the visuals based on the letter state
	/// </summary>
	void OnSubmit(string letters, LetterState[] letterStates)
	{
		letters = letters.ToLower();
		for(int i = 0; i < letters.Length; i++)
		{
			if(letters[i] == key)
			{
				if(letterStates[i] == LetterState.Missing)
				{
					background.gameObject.SetActive(false);
				}
				else
				{
					background.color = style.GetLetterColor(letterStates[i]);
				}
			}
		}
	}
}
