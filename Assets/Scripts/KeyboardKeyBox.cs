using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// an individual key on the keyboard, which will turn gray when it's found missing
public class KeyboardKeyBox : MonoBehaviour
{
	[SerializeField] TMP_Text text;
	[SerializeField] Image background;

	char key;

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

	void OnSubmit()
	{
		if(Wordle.ConfirmedMissingLetters.Contains(key))
		{
			background.gameObject.SetActive(false);
		}
	}
}
