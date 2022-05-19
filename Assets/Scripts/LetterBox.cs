using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// an individual letter of a row
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

	public void Initialize(int letterIndexInRow)
	{
		this.letterIndexInRow = letterIndexInRow;
	}

	// set the letter and state
	public void SetLetter(KeyCode letter, LetterState state)
	{
		// set the data
		Letter = letter;
		SetState(state);

		// update the UI
		PlayTypeAnimation();
		text.text = Letter.ToString().ToUpper();
	}

	// completely reset the letterbox
	public void Clear()
	{
		// reset the data
		SetState(LetterState.Empty);
		Letter = KeyCode.None;

		// reset the UI
		text.text = "";
		revealSequence.Complete();
		DOTween.Complete(all.transform);
	}

	// assign a state to this letterbox, and update the UI accordingly.
	// like if a letter is missing or correct.
	public void SetState(LetterState state)
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
			ApplyState(state);
		}
	}

	// apply a state, updating the UI accordingly
	void ApplyState(LetterState state)
	{
		background.color = style.GetLetterColor(state);
		background.sprite = style.GetBackgroundSprite(state);
		State = state;
	}

	// play the animation for when a letter is typed out
	void PlayTypeAnimation()
	{
		all.transform.DOScale(1.4f, 0f);
		all.transform.DOScale(1f, 0.3f).SetEase(Ease.OutExpo);
	}

	// play the animation for when a letter is not allowed
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

	// play the animation for revealing the result once the word
	// has been submitted
	void PlayRevealAnimation(LetterState state)
	{
		revealSequence = DOTween.Sequence();
		revealSequence.SetDelay(letterIndexInRow * 0.2f);
		revealSequence.Append(all.DOScaleY(0.001f, 0.2f));
		revealSequence.AppendCallback(() =>
		{
			ApplyState(state);
		});
		revealSequence.Append(all.DOScaleY(1f, 0.2f));
		revealSequence.Play();
	}
}
