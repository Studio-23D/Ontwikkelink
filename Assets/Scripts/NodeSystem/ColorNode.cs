using System;
using System.Reflection;
using UnityEngine;

public class ColorNode : Node
{
    private Color color = Color.white;

	public ColorNode(Vector2 position, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
	{
		this.rect = new Rect(position.x, position.y, 200, 100);
		this.style = nodeStyle;

		this.title = "Color";
		//inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
		//outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);

		this.outPoints.Add(new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, 1, OnClickOutPoint));
        this.outPoints.Add(new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, 2, OnClickOutPoint));

		this.defaultNodeStyle = nodeStyle;
		this.selectedNodeStyle = selectedStyle;
		this.OnRemoveNode = OnClickRemoveNode;
	}
}

