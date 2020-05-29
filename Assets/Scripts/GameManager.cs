using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private MusicController musicController;
	[SerializeField] private Button mute;
	[SerializeField] private Button nextSong;
    [SerializeField] private Button lastSong;
    [SerializeField] private Slider volumeSlider;

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
		nextSong.onClick.AddListener( delegate { controller.OtherSong(1); });
        lastSong.onClick.AddListener( delegate { controller.OtherSong(-1); });
        volumeSlider.onValueChanged.AddListener( delegate { controller.SetVolume(volumeSlider.value); });
    }
}
