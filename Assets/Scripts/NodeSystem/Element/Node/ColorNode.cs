using System;
using System.Reflection;
using UnityEngine;

public class ColorNode : Node
{
    private Color color = Color.white;

	public ColorNode(Vector2 position, float width, float height, Style style, Actions actions, int pointsInAmount, int pointsOutAmount)
	{
		this.title = "Color";
		this.rect = new Rect(position.x, position.y, width, height);

		this.style.nodeStyle = style.nodeStyle;
		this.style.defaultNodeStyle = style.nodeStyle;
		this.style.selectedNodeStyle = style.selectedNodeStyle;

		this.actions.OnRemoveNode = actions.OnRemoveNode;

		if (pointsInAmount > pointsOutAmount)
		{
			SetHeight(pointsInAmount);
		}
		else
		{
			SetHeight(pointsOutAmount);
		}

		for (int i = 0; i < pointsInAmount; i++)
		{
			float pointHeight = rect.height + (i * (rect.height / pointsInAmount));
			ConnectionPoint input = new ConnectionPoint(this, ConnectionPointType.In, style.pointStyle, pointHeight, actions.OnClickConnectionPoint);
			this.inPoints.Add(input);
		}

		for (int i = 0; i < pointsOutAmount; i++)
		{
			float pointHeight = rect.height + (i * (rect.height / pointsOutAmount));
			ConnectionPoint output = new ConnectionPoint(this, ConnectionPointType.Out, style.pointStyle, pointHeight, actions.OnClickConnectionPoint);
			this.outPoints.Add(output);
		}
	}
}
