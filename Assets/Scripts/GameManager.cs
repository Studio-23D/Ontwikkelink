using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private MusicController musicController;

	private void Start()
	{
		if (!FindObjectOfType<MusicController>())
		{
			Instantiate(musicController);
		}
	}
}
