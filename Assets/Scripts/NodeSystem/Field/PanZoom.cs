using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeSystem;

public class PanZoom : MonoBehaviour
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



	private void Awake()
	{
		inputManager.OnTouch += CheckTouch;
		//inputManager.OnScroll += Zoom;
	}

	public void SaveView()
	{
		foreach (Element element in nodeManager.GetElements)
		{
			if (element is Node)
			{
				Node node = (Node)element;

				node.SetStartPosition(node.Position);
			}
		}
	}

	public void ResetView()
	{
		foreach (Element element in nodeManager.GetElements)
		{
			if (element is Node)
			{
				Node node = (Node)element;

				node.ResetPosition();
			}
		}
	}



	private void CheckTouch(int touchCount, Vector3 startPosition, Transform selectedView)
	{
		if (selectedView != transform) return;

		if (touchCount == fingersToZoom)
		{
			//Zoom();
		}
		else
		{
			Pan(startPosition);
		}
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

	private void Pan(Vector3 startPosition)
	{
		// Pans when player swipes
		Vector3 direction = startPosition - inputManager.GetTouchPos;

#if UNITY_EDITOR
		direction = new Vector3(direction.x, -direction.y, direction.z);
#else
		direction = new Vector3(direction.x, direction.y, direction.z);
#endif

		foreach (Element element in nodeManager.GetElements)
		{
			if (element is Node)
			{
				Node node = (Node)element;

				if (node.isDragged)
				{
					return;
				}
			}
		}

		foreach (Element element in nodeManager.GetElements)
		{
			if (element is Node)
			{
				Node node = (Node)element;

				if (useSize)
				{
					node.Drag(direction * node.Size.x * panDifferenceModifier);
				}
				else
				{
					node.Drag(direction * panDifferenceModifier);
				}
			}
		}
	}

	private void ChangeZoom(float increment)
	{
		float scale = Mathf.Clamp(transform.localScale.x + increment, zoomMin, zoomMax);
		//transform.localScale = new Vector3(scale, scale, scale);
	}


	#region EDITOR

	/// <summary>
	/// Zooms by mouse scroll wheel
	/// </summary>
	/// <param name="scroll"></param>
	private void Zoom(float scroll)
	{
		ChangeZoom(scroll);
	}

	#endregion
}
