using UnityEngine;
using UnityEngine.UI;

public class HelpMenuItem : MonoBehaviour
{
	public TutorialType tutorialType;
	public Button button;
	public Sprite gif;
	public RuntimeAnimatorController animator;
	public HelpMenu helpMenu;

	private void Start()
	{
		button.onClick.AddListener(SetCurrentType);
	}

	private void SetCurrentType()
	{
		helpMenu.SetCurrentItem = this;
	}
}
