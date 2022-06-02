using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a keyboard which displays missing keys
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

	// clear all the keys on the keyboard and generate a new one
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

	// generate the keys on the keyboard
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
