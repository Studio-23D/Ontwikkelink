using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Tutorial
{
	public string label;
	public TutorialContainer feature;
	public GameObject activationView;
	[Tooltip("After this tutorial has finished, it will start the next tutorial with this label")]
	public string startTutorial;
}

public class TutorialManager : MonoBehaviour
{
	[SerializeField] private ViewManager viewManager;
	[SerializeField] private List<Tutorial> tutorials;

	private Tutorial currentTutorial;
	private GameObject currentTutorialPart;



	private void Awake()
	{
		viewManager.OnNewView += CheckNewView;
	}



	public void EnableTutorial(TutorialContainer tutorial)
	{
		PlayerPrefs.SetInt(tutorial.GetName, 0);
	}

	public void DisableTutorial(TutorialContainer tutorial)
	{
		PlayerPrefs.SetInt(tutorial.GetName, 1);
	}



	private void ContinueTutorial()
	{
		if (GetNextPart(currentTutorialPart))
		{
			currentTutorialPart.SetActive(false);
			currentTutorialPart = GetNextPart(currentTutorialPart);
			currentTutorialPart.SetActive(true);
		}
		else if (currentTutorial.startTutorial != "")
		{
			Destroy(currentTutorial.feature.gameObject);
			currentTutorial = GetTutorial(currentTutorial.startTutorial);
			InitTutorial(currentTutorial);
		}
		else
		{
			Destroy(currentTutorial.feature.gameObject);
		}
	}

	private void CheckNewView(GameObject view)
	{
		foreach (Tutorial tutorial in tutorials)
		{
			if (view != tutorial.activationView || !IsTutorialEnabled(tutorial)) continue;

			currentTutorial = tutorial;
			InitTutorial(currentTutorial);
		}
	}

	private void InitTutorial(Tutorial tutorial)
	{
		currentTutorial.feature = Instantiate(currentTutorial.feature, transform);
		currentTutorial.feature.GetContinueButton.onClick.AddListener(ContinueTutorial);

		if (currentTutorial.feature.GetParts.Count > 0)
		{
			currentTutorialPart = currentTutorial.feature.GetParts[0];
			currentTutorialPart.SetActive(true);
		}
		else
		{
			currentTutorialPart = null;
		}
	}

	private bool IsTutorialEnabled(Tutorial tutorial)
	{
		return PlayerPrefs.GetInt(tutorial.feature.GetName) == 0;
	}

	private bool IsLastPart(GameObject part)
	{
		return currentTutorialPart == currentTutorial.feature.GetLastPart;
	}

	private bool IsLastTutorial(Tutorial currentTutorial)
	{
		return tutorials.IndexOf(currentTutorial) == tutorials.Count - 1;
	}

	private GameObject GetNextPart(GameObject currentPart)
	{
		if (!IsLastPart(currentPart))
		{
			return currentTutorial.feature.GetParts[currentTutorial.feature.GetParts.IndexOf(currentPart) + 1];
		}
		else
		{
			return null;
		}
	}

	private Tutorial GetNextTutorial(Tutorial currentTutorial)
	{
		if (!IsLastTutorial(currentTutorial))
		{
			return tutorials[tutorials.IndexOf(currentTutorial) + 1];
		}
		else
		{
			return new Tutorial();
		}
	}


	private Tutorial GetTutorial(string label)
	{
		foreach (Tutorial tutorial in tutorials)
		{
			if (label != tutorial.label) continue;

			return tutorial;
		}

		return new Tutorial();
	}
}
