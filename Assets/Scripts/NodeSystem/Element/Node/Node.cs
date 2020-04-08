using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Node: Element
{
	public struct Style
	{
		public GUIStyle nodeStyle;
		public GUIStyle defaultNodeStyle;
		public GUIStyle selectedNodeStyle;
		public GUIStyle pointStyle;
	}

	public struct Actions
	{
		public Action<Node> OnRemoveNode;
		public Action<ConnectionPoint> OnClickConnectionPoint;
	}

	#region PUBLIC_MEMBERS

	public Style style;
	public Actions actions;
	public Rect rect;
    public string title;
	public bool isDragged;
	public bool isSelected;

	public List<ConnectionPoint> inPoints = new List<ConnectionPoint>();
	public List<ConnectionPoint> outPoints = new List<ConnectionPoint>();

	#endregion



	#region PUBLIC_METHODS

	public virtual void AddConnectionPoint()
    {

    }

	public void CalculateChange()
	{

	}

	public void Drag(Vector2 delta)
	{
		rect.position += delta;
	}

	public override void Draw()
    {
		if (inPoints.Count > 0)
		{
			foreach (ConnectionPoint point in inPoints)
			{
				point.Draw();
			}
		}

		if (outPoints.Count > 0)
		{
			foreach (ConnectionPoint point in outPoints)
			{
				point.Draw();
			}
		}

		style.nodeStyle.alignment = TextAnchor.UpperCenter;
		style.nodeStyle.contentOffset = new Vector2(0, 10f);
		GUI.Box(rect, title, style.nodeStyle);
	}

	/// <summary>
	/// Processes the player's interaction with the node
	/// </summary>
	/// <param name="e"></param>
	/// <returns></returns>
	public bool ProcessEvents(Event e)
	{
		switch (e.type)
		{
			case EventType.MouseDown:
				if (e.button == 0)
				{
					if (rect.Contains(e.mousePosition))
					{
						isDragged = true;
						GUI.changed = true;
						isSelected = true;
						style.nodeStyle = style.selectedNodeStyle;
					}
					else
					{
						GUI.changed = true;
						isSelected = false;
						style.nodeStyle = style.defaultNodeStyle;
					}
				}

				if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
				{
					ProcessContextMenu();
					e.Use();
				}
				break;

			case EventType.MouseUp:
				isDragged = false;
				break;

			case EventType.MouseDrag:
				if (e.button == 0 && isDragged)
				{
					Drag(e.delta);
					e.Use();
					return true;
				}
				break;
		}
		return false;
	}

	public void OnClickRemoveNode()
	{
		if (actions.OnRemoveNode != null)
		{
			actions.OnRemoveNode(this);
		}
	}

	public void SetHeight(int pointsCount)
	{
		/*if (inPoints.Count > outPoints.Count)
		{
			rect.height *= inPoints.Count;
		}
		else
		{
			rect.height *= outPoints.Count;
		}*/

		rect.height *= pointsCount;
	}

	#endregion



	#region PRIVATE_METHODS

	private void ProcessContextMenu()
	{
		GenericMenu genericMenu = new GenericMenu();
		genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
		genericMenu.ShowAsContext();
	}

	#endregion
}

#region BACK-UP
/*
    public Rect rect;
    public string title;
    public bool isDragged;
    public bool isSelected;

	public List<ConnectionPoint> inPoints = new List<ConnectionPoint>();
    //public ConnectionPoint inPoint;
	public List<ConnectionPoint> outPoints = new List<ConnectionPoint>();
    //public ConnectionPoint outPoint;

    public GUIStyle style;
    public GUIStyle defaultNodeStyle;
    public GUIStyle selectedNodeStyle;

    public Action<Node> OnRemoveNode;
      
    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    public void Draw()
    {
		if (inPoints.Count > 0)
		{
			foreach (ConnectionPoint point in inPoints)
			{
				point.Draw();
			}
		}

		if (outPoints.Count > 0)
		{
			foreach (ConnectionPoint point in outPoints)
			{
				point.Draw();
			}
		}

        //inPoint.Draw();
        //outPoint.Draw();
        style.alignment = TextAnchor.UpperCenter;
        style.contentOffset = new Vector2(0, 10f);
		GUI.Box(rect, title, style);
	}

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }

                if(e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }
        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode()
    {
        if (OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }
    */
#endregion