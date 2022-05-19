using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LetterState { Empty, Missing, Somewhere, Correct, Invalid, PreviouslyUsed };

// supplies the graphics for the wordle board
[CreateAssetMenu(menuName = "Style")]
public class Style : ScriptableObject
{
	[SerializeField] Sprite emptySprite;
	[SerializeField] Sprite filledSprite;
	[SerializeField] Color emptyColor;
	[SerializeField] Color missingColor;
	[SerializeField] Color somewhereColor;
	[SerializeField] Color correctColor;
	[SerializeField] Color invalidColor;
	[SerializeField] Color previouslyUsedColor;

	public Sprite GetBackgroundSprite(LetterState state)
	{
		return state == LetterState.Empty || state == LetterState.Invalid || state == LetterState.PreviouslyUsed ? emptySprite : filledSprite;
	}

	public Color GetLetterColor(LetterState state)
	{
		switch(state)
		{
			case LetterState.Missing:
				return missingColor;
			case LetterState.Correct:
				return correctColor;
			case LetterState.Somewhere:
				return somewhereColor;
			case LetterState.Invalid:
				return invalidColor;
			case LetterState.PreviouslyUsed:
				return previouslyUsedColor;
			case LetterState.Empty:
			default:
				return emptyColor;
		}
	}
}
