using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class Node: Element
{
    [InputPropperty]
    public Rect rect = new Rect(new Vector2(1,1), new Vector2(1, 1));
    public Action onRemove;

    
    public int aaaa = 0;

    public Node()
    {
        FieldInfo[] objectFields = this.GetType().GetFields();
        foreach (FieldInfo field in objectFields)
        {
            var attribute = Attribute.GetCustomAttribute(field, typeof(InputProppertyAttribute));
            Debug.Log(field.Name);
            if (attribute == null) return;
            Type type = attribute.GetType();
            Debug.Log(field.GetValue(this));
        }
    }

    public virtual void AddConnectionPoint<T>()
    {

    }

    public virtual void Drag()
    {

    }

    public override void Draw()
    {

    }
}
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
