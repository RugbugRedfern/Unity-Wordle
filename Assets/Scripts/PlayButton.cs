using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// a button which loads the game
public class PlayButton : MonoBehaviour
{
	public void Play()
	{
		SceneManager.LoadScene(1);
	}
}
