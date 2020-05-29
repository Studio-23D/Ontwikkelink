using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeSystem;
using System;

public class PanZoom : MonoBehaviour
{
	[Header("Zoom")]
	[SerializeField, Tooltip("Minimal zoom range")] private float zoomMin = 0.1f;
	[SerializeField, Tooltip("Maximal zoom range")] private float zoomMax = 2;
	[SerializeField, Tooltip("Modifies the difference between zoom steps")] private float zoomDifferenceModifier = 0.01f;

	[Header("Pan")]
	[SerializeField, Tooltip("Modifies the difference between pan steps")] private float panDifferenceModifier = 0.01f;

	[Header("References")]
	[SerializeField] private NodeManager nodeManager;

	private int fingersToZoom = 2;
	public bool isPanning = false;

	private Vector2 startPosition = new Vector2();

	public Action OnSave = delegate { };
	public Action OnReset = delegate { };

	private void Awake()
	{
		
	}

	public void SaveView()
	{
		OnSave.Invoke();
	}

	public void ResetView()
	{
		OnReset?.Invoke();
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

	public void Pan()
	{
		if (!isPanning)
		{
			startPosition = Event.current.mousePosition;
			isPanning = true;
		}

		Vector2 direction = Event.current.mousePosition - startPosition;
		direction.Normalize();
		direction *= panDifferenceModifier;
		Debug.Log(Event.current.mousePosition);


			// Pans when player swipes
			
			

#if UNITY_ANDROID
			direction = new Vector2(direction.x, direction.y);
#else
		direction = new Vector3(direction.x, -direction.y, 1);
#endif
		

		nodeManager.rect.position += direction;
		//nodeManager.EventHandeler.OnParrentChange.Invoke();
	}

	public void OnRelease()
	{
		isPanning = false;
	}


	private void OnGUI()
	{
		if (isPanning)
		GuiLineRenderer.DrawLine(startPosition, Event.current.mousePosition, Color.black, 20);
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
