using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
	/*
    private List<Node> node;
    private SystemEventHandeler eventHandeler;

    void Init()
    {
        node = new List<Node>();
        eventHandeler = new SystemEventHandeler();
        SystemEventHandeler.onInit.Invoke();
    }

    void InstaniateNode(Node node)
    {

        SystemEventHandeler.onNodeAdded.Invoke();
    }

    void RemoveNode()
    {

        SystemEventHandeler.onRemoveNode.Invoke();
    }
}*/


	[SerializeField]
	private Texture2D nodeBackground;
	[SerializeField]
	private Texture2D pointBackground;

	[SerializeField]
	private int pointsInAmount;
	[SerializeField]
	private int pointsOutAmount;

	private List<Node> nodes;
	private List<Connection> connections;

	private GUIStyle nodeStyle;
	private GUIStyle selectedNodeStyle;
	private GUIStyle pointStyle;
	private GUIStyle inPointStyle;
	private GUIStyle outPointStyle;

	private ConnectionPoint selectedInPoint;
	private ConnectionPoint selectedOutPoint;

	private void OnEnable()
	{
		nodeStyle = new GUIStyle();
		nodeStyle.normal.background = nodeBackground;
		nodeStyle.border = new RectOffset(12, 12, 12, 12);

		selectedNodeStyle = new GUIStyle();
		selectedNodeStyle.normal.background = nodeBackground;
		selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

		pointStyle = new GUIStyle();
		pointStyle.normal.background = pointBackground;
		pointStyle.active.background = pointBackground;
		pointStyle.border = new RectOffset(4, 4, 12, 12);

		inPointStyle = new GUIStyle();
		inPointStyle.normal.background = pointBackground;
		inPointStyle.active.background = pointBackground;
		inPointStyle.border = new RectOffset(4, 4, 12, 12);

		outPointStyle = new GUIStyle();
		outPointStyle.normal.background = pointBackground;
		outPointStyle.active.background = pointBackground;
		outPointStyle.border = new RectOffset(4, 4, 12, 12);
	}

	private void OnGUI()
	{
		DrawNodes();
		DrawConnections();

		DrawConnectionLine(Event.current);

		ProcessNodeEvents(Event.current);
		ProcessEvents(Event.current);
	}

	private void DrawNodes()
	{
		if (nodes != null)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				nodes[i].Draw();
			}
		}
	}

	private void DrawConnections()
	{
		if (connections != null)
		{
			for (int i = 0; i < connections.Count; i++)
			{
				connections[i].Draw();
			}
		}
	}

	private void DrawConnectionLine(Event e)
	{
		if (selectedInPoint != null && selectedOutPoint == null)
		{
			Handles.DrawBezier(
				selectedInPoint.rect.center,
				e.mousePosition,
				selectedInPoint.rect.center + Vector2.left * 50f,
				e.mousePosition - Vector2.left * 50f,
				Color.white,
				null,
				2f
			);

			GUI.changed = true;
		}

		if (selectedOutPoint != null && selectedInPoint == null)
		{
			Handles.DrawBezier(
				selectedOutPoint.rect.center,
				e.mousePosition,
				selectedOutPoint.rect.center - Vector2.left * 50f,
				e.mousePosition + Vector2.left * 50f,
				Color.white,
				null,
				2f
			);

			GUI.changed = true;
		}
	}

	private void ProcessEvents(Event e)
	{
		switch (e.type)
		{
			case EventType.MouseDown:
				if (e.button == 1)
				{
					ProcessContextMenu(e.mousePosition);
				}
				break;
		}
	}

	private void ProcessNodeEvents(Event e)
	{
		if (nodes != null)
		{
			for (int i = nodes.Count - 1; i >= 0; i--)
			{
				bool guiChanged = nodes[i].ProcessEvents(e);

				if (guiChanged)
				{
					GUI.changed = true;
				}
			}
		}
	}

	private void ProcessContextMenu(Vector2 mousePosition)
	{
		GenericMenu genericMenu = new GenericMenu();
		genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition));
		genericMenu.ShowAsContext();
	}

	private void OnClickAddNode(Vector2 mousePosition)
	{
		if (nodes == null)
		{
			nodes = new List<Node>();
		}

		Node.Style nodeStyleContainer = new Node.Style();
		nodeStyleContainer.nodeStyle = nodeStyle;
		nodeStyleContainer.selectedNodeStyle = selectedNodeStyle;
		nodeStyleContainer.pointStyle = pointStyle;

		Node.Actions actionsContainer = new Node.Actions();
		actionsContainer.OnRemoveNode = OnClickRemoveNode;
		actionsContainer.OnClickConnectionPoint = OnClickOutPoint;

		nodes.Add(new ColorNode(mousePosition, 200, 50, nodeStyleContainer, actionsContainer, pointsInAmount, pointsOutAmount));

		//nodes.Add(new ColorNode(mousePosition, 200, 50, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode));
	}

	private void OnClickInPoint(ConnectionPoint inPoint)
	{
		selectedInPoint = inPoint;

		if (selectedOutPoint != null)
		{
			if (selectedOutPoint.node != selectedInPoint.node)
			{
				CreateConnection();
				ClearConnectionSelection();
			}
			else
			{
				ClearConnectionSelection();
			}
		}
	}

	private void OnClickOutPoint(ConnectionPoint outPoint)
	{
		selectedOutPoint = outPoint;

		if (selectedInPoint != null)
		{
			if (selectedOutPoint.node != selectedInPoint.node)
			{
				CreateConnection();
				ClearConnectionSelection();
			}
			else
			{
				ClearConnectionSelection();
			}
		}
	}

	private void OnClickRemoveConnection(Connection connection)
	{
		connections.Remove(connection);
	}

	private void CreateConnection()
	{
		if (connections == null)
		{
			connections = new List<Connection>();
		}

		connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
	}

	private void ClearConnectionSelection()
	{
		selectedInPoint = null;
		selectedOutPoint = null;
	}

	private void OnClickRemoveNode(Node node)
	{
		nodes.Remove(node);
	}
}