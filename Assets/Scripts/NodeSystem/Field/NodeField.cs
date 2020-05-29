using UnityEngine;
using NodeSystem;
using System;

public class NodeField : MonoBehaviour
{
	[Header("Zoom")]
	[SerializeField, Tooltip("Minimal zoom range")] private float zoomMin = 0.1f;
	[SerializeField, Tooltip("Maximal zoom range")] private float zoomMax = 2;
	[SerializeField, Tooltip("Modifies the difference between zoom steps")] private float zoomDifferenceModifier = 0.01f;

	[Header("Pan")]
	[SerializeField, Tooltip("Modifies the difference between pan steps")] private float panDifferenceModifier = 0.01f;
	[SerializeField, Tooltip("Uses size to calculate pan speed")] private bool useSize = false;

	[Header("References")]
	[SerializeField] private InputManager inputManager;
	[SerializeField] private NodeManager nodeManager;

	private int fingersToZoom = 2;
	public bool isDragging = false;

	private Vector2 startPosition = new Vector2();

	public Action OnSave = delegate { };
	public Action OnReset = delegate { };
	public Action OnDrag = delegate { };
	public Action OnRelease = delegate { };

	private void Awake()
	{
		//inputManager.OnTouch += CheckTouch;
	}

	public void SaveView()
	{
		OnSave.Invoke();
	}

	public void ResetView()
	{
		OnReset?.Invoke();
	}

	private void CheckTouch(int touchCount, Vector3 startPosition, Transform selectedView)
	{
		if (selectedView != transform)
		{
			EnableFieldDrag(false);
			return;
		}

		if (touchCount == fingersToZoom)
		{
			//Zoom();
		}
		else
		{
			EnableFieldDrag(true);
		}
	}

	private void EnableFieldDrag(bool enable)
	{
		/*if (nodeManager.AreNodesDragged)
		{
			nodeManager.DraggingAllNodes = false;
			return;
		}

		nodeManager.DraggingAllNodes = enable;

		foreach (Element element in nodeManager.GetElements)
		{
			if (element is Node)
			{
				Node node = (Node)element;

				node.isDragged = enable;
			}
		}*/


	}

	private void Zoom()
	{
		// Zooms when player pinches
		Touch touchZero = Input.GetTouch(0);
		Touch touchOne = Input.GetTouch(1);

		Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

		float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

		float difference = currentMagnitude - prevMagnitude;

		ChangeZoom(difference * zoomDifferenceModifier);
	}

	private void ChangeZoom(float increment)
	{
		float scale = Mathf.Clamp(transform.localScale.x + increment, zoomMin, zoomMax);
		//transform.localScale = new Vector3(scale, scale, scale);
	}
}
