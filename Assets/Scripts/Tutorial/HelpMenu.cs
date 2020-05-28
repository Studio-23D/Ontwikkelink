using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
	public HelpMenuItem SetCurrentItem
	{
		set
		{
			currentItem = value;
			InitItem(currentItem);
		}
	}

	[SerializeField] private TutorialManager tutorialManager;
	[SerializeField] private Toggle toggle;
	[SerializeField] private Image gifImage;
	[SerializeField] private Animator gifAnimator;

	private HelpMenuItem currentItem;



	private void OnEnable()
	{
		CheckToggle(true);
	}

	private void OnDisable()
	{
		CheckToggle(false);
		gifImage.gameObject.SetActive(false);
	}



	private void InitItem(HelpMenuItem item)
	{
		ActivateExample(currentItem.gif, currentItem.animator);
	}

	private void ActivateExample(Sprite gif, RuntimeAnimatorController animator)
	{
		gifAnimator.runtimeAnimatorController = animator;
		gifImage.sprite = gif;
		gifImage.gameObject.SetActive(true);
	}

	public void CheckToggle(bool onEnable)
	{
		if (onEnable)
		{
			if (!tutorialManager.StartTutorials)
			{
				toggle.isOn = true;
			}
			else
			{
				toggle.isOn = false;
			}
		}
		else
		{
			if (!toggle.isOn)
			{
				tutorialManager.EnableAllTutorials();
			}
			else
			{
				tutorialManager.DisableAllTutorials();
			}
		}
	}
}
