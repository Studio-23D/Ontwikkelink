using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionPointType { In, Out }

public class ConnectionPoint
{
	public Rect rect;

    public ConnectionPointType type;

    public Node node;

    public GUIStyle style;

    public float height;

	public Action<ConnectionPoint> OnClickConnectionPoint;

    public ConnectionPoint(Node node, ConnectionPointType type, GUIStyle style, float height, Action<ConnectionPoint> OnClickConnectionPoint)
    {
        this.node = node;
        this.type = type;
        this.style = style;
        this.height = height;
		this.OnClickConnectionPoint = OnClickConnectionPoint;

		rect = new Rect(0, 0, 20f, 20f);
	}

	public void Draw()
    {
		//rect.y = node.rect.y + (30 * index);
		//rect.y = node.rect.y + (index * (node.rect.height / maxIndex));
		rect.y = node.rect.y - node.rect.height + height + rect.height;

		switch (type)
        {
            case ConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8f;
                break;

            case ConnectionPointType.Out:
                rect.x = node.rect.x + node.rect.width - 8f;
                break;
        }

        if (GUI.Button(rect, "", style))
        {
            if(OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }
    }
}
