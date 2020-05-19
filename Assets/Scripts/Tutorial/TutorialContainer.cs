using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialContainer : MonoBehaviour
{
	public string GetName => name;
	public GameObject GetFirstPart
	{
		get
		{
			if (parts.Count > 0)
			{
				return parts[0];
			}
			else
			{
				return null;
			}
		}
	}
	public GameObject GetLastPart
	{
		get
		{
			if (parts.Count > 0)
			{
				return parts[parts.Count - 1];
			}
			else
			{
				return null;
			}
		}
	}
	public List<GameObject> GetParts => parts;
	public Button GetContinueButton => continueButton;

	[SerializeField] private List<GameObject> parts;
	[SerializeField] private Button continueButton;
}
