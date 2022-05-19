using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// loads a background image for the game if one is found in the files
public class BackgroundImage : MonoBehaviour
{
	[SerializeField] RawImage rawImage;

	void Start()
	{
		var path = Path.Combine(Application.dataPath, "..", "background.png");
		if(File.Exists(path))
		{
			StartCoroutine(LoadImage(path));
		}
	}

	IEnumerator LoadImage(string url)
	{
		using(UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
		{
			yield return uwr.SendWebRequest();

			if(uwr.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError(uwr.error);
			}
			else
			{
				var texture = DownloadHandlerTexture.GetContent(uwr);
				rawImage.texture = texture;
				rawImage.enabled = true;
			}
		}
	}
}
