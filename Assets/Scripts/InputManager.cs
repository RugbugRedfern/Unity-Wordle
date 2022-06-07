using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle player input for the entire game
/// </summary>
public class InputManager : MonoBehaviour
{
	KeyCode[] letters = new KeyCode[] { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z };
	KeyCode deleteKey = KeyCode.Backspace;
	KeyCode submitKey = KeyCode.Return;

	public static event Action<KeyCode> OnLetter;
	public static event Action OnDelete;
	public static event Action OnSubmit;

	void Update()
	{
		for(int i = 0; i < letters.Length; i++)
		{
			if(Input.GetKeyDown(letters[i]))
			{
				OnLetter?.Invoke(letters[i]);
			}
		}

		if(Input.GetKeyDown(deleteKey))
		{
			OnDelete?.Invoke();
		}

		if(Input.GetKeyDown(submitKey))
		{
			OnSubmit?.Invoke();
		}
	}
}
