using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// a row of the wordle board
public class Row : MonoBehaviour
{
	public static event Action OnSubmit;

	[SerializeField] LetterBox[] letterBoxes;

	public bool Full => inputIndex == letterBoxes.Length;

	int inputIndex;

	void Start()
	{
		// initialize each letter box with its index in the row
		for(int i = 0; i < letterBoxes.Length; i++)
		{
			letterBoxes[i].Initialize(i);
		}
	}

	public bool TryAddLetter(KeyCode letter)
	{
		// we can't add a letter if the row is full
		if(Full)
		{
			return false;
		}

		// convert the KeyCode to a char
		char letterChar = letter.ToString().ToLower().ToCharArray()[0];

		// assign the letter to the current letter box
		letterBoxes[inputIndex].SetLetter(
			letter,
			Wordle.ConfirmedMissingLetters.Contains(letterChar)
				? LetterState.PreviouslyUsed : LetterState.Empty
		);

		// increment the input index to the next letter box
		inputIndex++;
		return true;
	}

	// completely reset the row
	public void Clear()
	{
		for(int i = 0; i < letterBoxes.Length; i++)
		{
			letterBoxes[i].Clear();
		}
		
		inputIndex = 0;
	}

	// delete the last letter from the row
	public void DeleteLetter()
	{
		inputIndex--;
		inputIndex = Mathf.Max(inputIndex, 0);
		letterBoxes[inputIndex].Clear();
	}

	// convert the row to a string and validate it
	public void Submit()
	{
		// convert the row into a string
		var sb = new StringBuilder();
		for(int i = 0; i < letterBoxes.Length; i++)
		{
			sb.Append(letterBoxes[i].Letter);
		}

		// validate the row, and assign the states to the letter boxes
		LetterState[] letterStates = Wordle.Instance.ValidateRow(sb.ToString());
		for(int i = 0; i < letterBoxes.Length; i++)
		{
			letterBoxes[i].SetState(letterStates[i]);
		}

		OnSubmit?.Invoke();
	}
}