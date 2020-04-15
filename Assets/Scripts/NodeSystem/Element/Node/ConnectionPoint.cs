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
		Rect nodeMidRect = node.sections.midRect;
		rect.y = nodeMidRect.y + height - (rect.height / 2);

		switch (type)
        {
            case ConnectionPointType.In:
                rect.x = nodeMidRect.x - rect.width + 8f;
                break;

            case ConnectionPointType.Out:
                rect.x = nodeMidRect.x + nodeMidRect.width - 8f;
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
