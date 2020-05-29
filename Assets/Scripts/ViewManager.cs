///
/// ViewHandler will be called when action is required around menus
///

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NodeSystem;

public class ViewManager : MonoBehaviour
{
	public Action<GameObject> OnNewView;
	public Action<bool> OnReset;
	public List<GameObject> UsedViews { get { return usedViews; } set { usedViews = value; } }
	public GameObject CurrentView { get { return currentView; } set { currentView = value; } }
	public GameObject MainMenu { get { return mainMenu; } set { mainMenu = value; } }
	public GameObject PreviousView { get { return usedViews[usedViews.Count - 2]; } }
	public GameObject LastView { get { return usedViews[usedViews.Count - 1]; } }
	public GameObject SetBuffer { set { viewBuffer = value; } }

	[Header("View References")]
	[SerializeField] private NodeManager nodeManager;
	[SerializeField] private Button quitButton;

	[Header("View References")]
	[SerializeField] private GameObject currentView = null;
	[SerializeField] private GameObject mainMenu = null;
	[SerializeField] private GameObject characterMenu = null;
	[SerializeField] private GameObject DesignView = null;

	[Header("Variables")]
	[Tooltip("All used menus with home menu as start")]
	[SerializeField] private List<GameObject> usedViews = new List<GameObject>();
	[Tooltip("All active menus that are shown to the user")]
	[SerializeField] private List<GameObject> activeViews = new List<GameObject>();
	[SerializeField] private string overlayTag = "Overlay";

	private GameObject viewBuffer = null;



	private void Start()
	{
		Init();
	}



	public void ChangeViewTo(GameObject newView)
	{
		if (newView == DesignView)
		{
			nodeManager.ToggleDraw(true);
		} else if (LastView == DesignView) {
			nodeManager.ToggleDraw(false);
		}


		if (newView == mainMenu)
		{
			ResetScene();
		}
		else if (newView == characterMenu && currentView != mainMenu)
		{
			ResetScene(1);
		}

		if (!usedViews.Contains(newView))
		{
			usedViews.Add(newView);
		}
		else
		{
			usedViews.Remove(currentView);
		}

		currentView.SetActive(false);

		if (newView.tag != overlayTag)
		{
			foreach (GameObject view in activeViews)
			{
				view.SetActive(false);
			}

			activeViews.Clear();
			activeViews.Add(newView);
		}
		else
		{
			if (currentView.tag == overlayTag)
			{
				currentView.SetActive(false);
			}
			else
			{
				currentView.SetActive(true);
			}

			if (!activeViews.Contains(newView))
			{
				activeViews.Add(newView);
			}
		}

		if (OnNewView != null)
			OnNewView(newView);

		newView.SetActive(true);
		currentView = newView;
		
		while (currentView != LastView)
		{
			RemoveLastView();
		}
	}

	public void SetViewTo(GameObject newView)
	{
		if (!usedViews.Contains(newView))
		{
			usedViews.Add(newView);
		}
		else
		{
			usedViews.Remove(currentView);
		}

		if (activeViews.Contains(currentView))
		{
			activeViews.Remove(currentView);
		}

		if (OnNewView != null)
			OnNewView(newView);

		newView.SetActive(true);
		currentView = newView;

		if (!activeViews.Contains(currentView))
		{
			activeViews.Add(currentView);
		}
	}

	public void ChangeViewToBuffer()
	{
		ChangeViewTo(viewBuffer);
	}

	/// <summary>
	/// Removes last menu in usedViews list
	/// </summary>
	private void RemoveLastView()
	{
		usedViews.Remove(LastView);
	}

	public void Return()
	{
		if (usedViews.Count == 1)
		{
			Application.Quit();
			return;
		}

		ChangeViewTo(PreviousView);
	}

	public void Quit()
	{
		Application.Quit();
	}



	private void Init()
	{
		usedViews.Add(mainMenu);
		activeViews.Add(mainMenu);

		if (PlayerPrefs.GetInt(characterMenu.name) == 1)
		{
			ChangeViewTo(characterMenu);
			PlayerPrefs.DeleteKey(characterMenu.name);
		}

		if (SystemInfo.deviceType != DeviceType.Handheld)
		{
			quitButton.gameObject.SetActive(false);
		}
	}


	#region SCENE_METHODS

	/// <summary>
	/// Call method to change scene async
	/// </summary>
	/// <param name="name"></param>
	public void ChangeScene(string sceneName)
	{
		StartCoroutine(ExitScene(sceneName));
	}


	public void ResetScene()
	{
		if (OnReset != null)
			OnReset(true);

		StartCoroutine(ExitScene(SceneManager.GetActiveScene().name));
	}

	public void ResetScene(int openCharacterMenu)
	{
		if (OnReset != null)
			OnReset(true);

		PlayerPrefs.SetInt(characterMenu.name, openCharacterMenu);
		StartCoroutine(ExitScene(SceneManager.GetActiveScene().name));
	}

	/// <summary>
	/// Changes scene async
	/// </summary>
	/// <param name="NewScene"></param>
	/// <returns></returns>
	private IEnumerator ExitScene(string sceneName)
	{
		yield return SceneManager.LoadSceneAsync(sceneName);
	}

	#endregion
}
