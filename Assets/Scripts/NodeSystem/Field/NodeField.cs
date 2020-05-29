using UnityEngine;
using NodeSystem;
using System;
using System.Collections.Generic;

public class NodeField : MonoBehaviour
{
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

	public void Drag()
	{
		if (inputManager.SelectedView != transform || IsNodeDragged()) return;

		isDragging = true;
		OnDrag?.Invoke();
	}

	public void Release()
	{
		isDragging = false;
		OnRelease?.Invoke();
	}

	private bool IsNodeDragged()
	{
		for (int i = 0; i < nodeManager.Elements.Count; i++)
		{
			if (nodeManager.Elements[i].IsBeingDragged)
			{
				Debug.Log(nodeManager.Elements[i] + " being dragged: " + nodeManager.Elements[i].IsBeingDragged);

				return true;
			}
		}

		return false;
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
}
