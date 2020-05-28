using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private MusicController musicController;
	[SerializeField] private Button mute;
	[SerializeField] private Button nextSong;

	private void Start()
	{
		InitMusic();
	}

	private void InitMusic()
	{
		MusicController controller;

		if (!FindObjectOfType<MusicController>())
		{
			controller = Instantiate(musicController);
		}
		else
		{
			controller = FindObjectOfType<MusicController>();
		}

		mute.onClick.AddListener(controller.ToggleMusic);
		nextSong.onClick.AddListener(controller.NextSong);
	}
}
