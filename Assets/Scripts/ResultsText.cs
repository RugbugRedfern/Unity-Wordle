using TMPro;
using UnityEngine;

/// <summary>
/// Displays the results at the end of the game
/// </summary>
public class ResultsText : MonoBehaviour
{
	void Start()
	{
		Timer.OnTimeExpired += OnTimeExpired;
	}

	void OnDestroy()
	{
		Timer.OnTimeExpired -= OnTimeExpired;
	}

	/// <summary>
	/// Called then the game timer has expired. Updates the results text with the player's score
	/// </summary>
	void OnTimeExpired()
	{
		GetComponent<TMP_Text>().text = $"Time Expired\n\nScore: {Wordle.Instance.GetWins()}\n\nPress Enter to Restart";
	}
}
