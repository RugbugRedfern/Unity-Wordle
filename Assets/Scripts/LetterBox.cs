using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// An individual letter of a row
/// </summary>
public class LetterBox : MonoBehaviour
{
	[SerializeField] Style style;
	[SerializeField] TMP_Text text;
	[SerializeField] Image background;
	[SerializeField] RectTransform all;

	public LetterState State { get; private set; }
	public KeyCode Letter { get; private set; }

	int letterIndexInRow;

	Sequence revealSequence;

	/// <summary>
	/// Construct the LetterBox
	/// </summary>
	/// <param name="letterIndexInRow">The index of this letter box in the row that created it</param>
	public void Initialize(int letterIndexInRow)
	{
		this.letterIndexInRow = letterIndexInRow;
	}

	/// <summary>
	/// Set the letter and state
	/// </summary>
	/// <param name="letter">The letter to assign</param>
	/// <param name="state">The state to assign</param>
	public void SetLetter(KeyCode letter, LetterState state)
	{
		// set the data
		Letter = letter;
		SetAnimatedState(state);

		// update the UI
		PlayTypeAnimation();
		text.text = Letter.ToString().ToUpper();
	}

	/// <summary>
	/// Completely reset the letterbox
	/// </summary>
	public void Clear()
	{
		// reset the data
		SetAnimatedState(LetterState.Empty);
		Letter = KeyCode.None;

		// reset the UI
		text.text = "";
		revealSequence.Complete();
		DOTween.Complete(all.transform);
	}

	/// <summary>
	/// Assign a state to this letterbox. Play an animation if it's available
	/// </summary>
	/// <param name="state">The state to assign</param>
	public void SetAnimatedState(LetterState state)
	{
		if(state == LetterState.Invalid)
		{
			PlayInvalidAnimation();
		}
		else if(state != LetterState.Empty && state != LetterState.PreviouslyUsed)
		{
			PlayRevealAnimation(state);
		}
		else
		{
			SetState(state);
		}
	}

	/// <summary>
	/// Apply a state instantly, updating the UI accordingly
	/// </summary>
	/// <param name="state">The state to assign</param>
	void SetState(LetterState state)
	{
		background.color = style.GetLetterColor(state);
		background.sprite = style.GetBackgroundSprite(state);
		State = state;
	}

	/// <summary>
	/// Play the animation for when a letter is typed out
	/// </summary>
	void PlayTypeAnimation()
	{
		all.transform.DOScale(1.4f, 0f);
		all.transform.DOScale(1f, 0.3f).SetEase(Ease.OutExpo);
	}

	/// <summary>
	/// Play the animation for when a letter is not allowed
	/// </summary>
	void PlayInvalidAnimation()
	{
		var seq = DOTween.Sequence();
		seq.Append(all.DOLocalMoveX(-20f, 0.1f));
		seq.Append(all.DOLocalMoveX(15f, 0.1f));
		seq.Append(all.DOLocalMoveX(-10f, 0.1f));
		seq.Append(all.DOLocalMoveX(5f, 0.1f));
		seq.Append(all.DOLocalMoveX(0f, 0.1f));
		seq.Play();
	}

	/// <summary>
	/// play the animation for revealing the result once the word has been submitted
	/// </summary>
	/// <param name="state">The state to set the LetterBox to while it is hidden in the middle of the animation</param>
	void PlayRevealAnimation(LetterState state)
	{
		revealSequence = DOTween.Sequence();
		revealSequence.SetDelay(letterIndexInRow * 0.2f);
		revealSequence.Append(all.DOScaleY(0.001f, 0.2f));
		revealSequence.AppendCallback(() =>
		{
			SetState(state);
		});
		revealSequence.Append(all.DOScaleY(1f, 0.2f));
		revealSequence.Play();
	}
}
