using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Allows the game to be restarted while in progress by pressing CTRL + SHIFT + R for demonstration purposes
/// </summary>
public class RestartGame : MonoBehaviour
{
	void Update()
	{
		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R))
		{
			SceneManager.LoadScene(0);
		}
	}
}
