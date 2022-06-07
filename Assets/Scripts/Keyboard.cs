using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An in-game keyboard which displays missing keys
/// </summary>
public class Keyboard : MonoBehaviour
{
	public static Keyboard Instance;

	string[] layout = new string[]
	{
		"qwertyuiop",
		"asdfghjkl",
		"zxcvbnm"
	};

	[SerializeField] Transform[] rows;
	[SerializeField] GameObject usedKeyPrefab;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Generate();
	}

	/// <summary>
	/// Clear all the keys on the keyboard and generate a new one
	/// </summary>
	public void Clear()
	{
		foreach(var row in rows)
		{
			foreach(Transform child in row)
			{
				Destroy(child.gameObject);
			}
		}

		Generate();
	}

	/// <summary>
	/// Generate the keys on the keyboard
	/// </summary>
	void Generate()
	{
		for(int i = 0; i < layout.Length; i++)
		{
			foreach(var key in layout[i])
			{
				Instantiate(usedKeyPrefab, rows[i]).GetComponent<KeyboardKeyBox>().Initialize(key);
			}
		}
	}
}
