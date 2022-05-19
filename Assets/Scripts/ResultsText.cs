using TMPro;
using UnityEngine;

// displays the results at the end of the game
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

	void OnTimeExpired()
	{
		GetComponent<TMP_Text>().text = $"Time Expired\n\nScore: {Wordle.Instance.GetWins()}\n\nPress Enter to Restart";
	}
}
