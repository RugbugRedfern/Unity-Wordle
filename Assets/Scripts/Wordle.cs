using DG.Tweening;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

// handles the game logic and score tracking
public class Wordle : MonoBehaviour
{
	public static Wordle Instance { get; private set; }

	[SerializeField] RectTransform wordleContainer;
	[SerializeField] Animator animator;
	[SerializeField] TMP_Text correctAnswerText;
	[SerializeField] TMP_Text winText;
	[SerializeField] TMP_Text winIncreaseText;

	// the wordle data (answers and valid words), loaded from a JSON file
	WordData wordData;

	Row[] rows;
	int rowIndex;

	string currentWord;
	Dictionary<char, int> letterCounts = new Dictionary<char, int>();

	bool paused;

	int wins;

	// letters that the player has guessed which do not appear in the answer
	public static List<char> ConfirmedMissingLetters { get; private set; } = new List<char>();

	void OnEnable()
	{
		InputManager.OnLetter += OnLetter;
		InputManager.OnDelete += OnDelete;
		InputManager.OnSubmit += OnSubmit;
	}

	void OnDisable()
	{
		InputManager.OnLetter -= OnLetter;
		InputManager.OnDelete -= OnDelete;
		InputManager.OnSubmit -= OnSubmit;
	}

	void Awake()
	{
		rows = GetComponentsInChildren<Row>();
		Instance = this;
	}

	void Start()
	{
		// load the words from the json file
		wordData = JsonConvert.DeserializeObject<WordData>(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "data.json")));

		Clear();
	}

	// if the user presses a key, try to add it to the current row
	void OnLetter(KeyCode letter)
	{
		if(paused)
			return;

		rows[rowIndex].TryAddLetter(letter);
	}

	// if the user presses the delete key, delete a letter from the row
	void OnDelete()
	{
		rows[rowIndex].DeleteLetter();
	}

	// if the user presses the submit key, submit or clear if the game is over
	void OnSubmit()
	{
		if(paused)
			return;

		rows[rowIndex].Submit();
	}

	// validate a row and return the state of each letter in the row
	public LetterState[] ValidateRow(string content)
	{
		LetterState[] letterStates = new LetterState[content.Length];

		// make the content lowercase so we can make sure comparisons ignore case
		content = content.ToLower();

		// if the word is not a real word
		if(!wordData.words.Contains(content) && !wordData.answers.Contains(content))
		{
			for(int i = 0; i < letterStates.Length; i++)
			{
				letterStates[i] = LetterState.Invalid;
			}
			return letterStates;
		}

		// check if it's the correct word
		if(content == currentWord)
		{
			CompleteGame(true);
		}

		// tells us how many times we have processed each letter.
		var processedLetterCounts = new Dictionary<char, int>();
		// initialize the starting count to 0
		for(int i = 0; i < content.Length; i++)
		{
			processedLetterCounts[content[i]] = 0;
		}

		// loop through the letters and mark the correct ones
		for(int i = 0; i < content.Length; i++)
		{
			char letter = content[i];
			char correctLetter = currentWord[i];

			if(letter == correctLetter)
			{
				letterStates[i] = LetterState.Correct;
				processedLetterCounts[letter]++;
			}
		}

		// loop through the letters and mark the "somewhere" ones
		for(int i = 0; i < content.Length; i++)
		{
			// skip over correct letters, we've already processed those
			if(letterStates[i] == LetterState.Correct)
				continue;

			char letter = content[i];

			if(currentWord.Contains(letter) && processedLetterCounts[letter] < letterCounts[letter])
			{
				letterStates[i] = LetterState.Somewhere;
				processedLetterCounts[letter]++;
			}
			else
			{
				letterStates[i] = LetterState.Missing;

				// record missing letters
				if(!ConfirmedMissingLetters.Contains(letter) && !currentWord.Contains(letter))
				{
					ConfirmedMissingLetters.Add(letter);
				}
			}
		}

		// proceed to the next row
		rowIndex++;

		// if we run out of rows
		if(rowIndex == Instance.rows.Length)
		{
			CompleteGame(false);
		}

		return letterStates;
	}

	void CompleteGame(bool won)
	{
		paused = true;
		StartCoroutine(I_PlayClearAnimation(2f, won));

		if(!won)
			return;

		// calculate the score increase
		int score = 6 - rowIndex;
		wins += score;

		// update the UI
		winText.text = wins.ToString();
		winIncreaseText.text = $"+{score}";
		animator.SetTrigger("win");
	}

	public int GetWins()
	{
		return wins;
	}

	// play an animation of the wordle board sliding out and back in, clearing when it's off screen
	IEnumerator I_PlayClearAnimation(float delay, bool won)
	{
		// pause the timer so it doesn't go down while the animation is playing
		Timer.Instance.Pause();

		yield return new WaitForSeconds(delay);

		wordleContainer.DOAnchorPosX((Screen.width + wordleContainer.rect.width) / 2f, 1f).SetEase(Ease.InExpo);

		yield return new WaitForSeconds(1f);

		if(!won)
		{
			correctAnswerText.DOFade(1f, 0.3f);
			yield return new WaitForSeconds(2.3f);
			correctAnswerText.DOFade(0f, 0.3f);
			yield return new WaitForSeconds(0.3f);
		}

		Clear();
		Keyboard.Instance.Clear();

		wordleContainer.DOAnchorPosX(-(Screen.width + wordleContainer.rect.width) / 2f, 0f);
		wordleContainer.DOAnchorPosX(0, 1f).SetEase(Ease.OutExpo);

		yield return new WaitForSeconds(1f);

		// resume the timer when the animation is complete
		Timer.Instance.Resume();
	}

	// clear the entire wordle board and generate a new word
	void Clear()
	{
		// clear all the rows
		foreach(var row in rows)
		{
			row.Clear();
		}

		rowIndex = 0;

		// choose a new current word
		currentWord = wordData.answers[Random.Range(0, wordData.answers.Length)];
		correctAnswerText.text = $"The word was\n<size=60>{currentWord.ToUpper()}</size>";
		Debug.Log(currentWord);

		// calculate the occurrences of each letter in the word
		letterCounts.Clear();
		for(int i = 0; i < currentWord.Length; i++)
		{
			char letter = currentWord[i];

			if(!letterCounts.ContainsKey(letter))
				letterCounts[letter] = 1;
			else
				letterCounts[letter]++;
		}

		ConfirmedMissingLetters.Clear();

		paused = false;
	}

	// class for all the wordle data, loaded from a JSON file
	class WordData
	{
		public string[] answers;
		public string[] words;
	}
}
