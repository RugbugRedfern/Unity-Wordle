using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// allows the game to be restarted while in progress for demonstration purposes
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
