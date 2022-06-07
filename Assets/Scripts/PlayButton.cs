using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A button which loads the game
/// </summary>
public class PlayButton : MonoBehaviour
{
	public void Play()
	{
		SceneManager.LoadScene(1);
	}
}
