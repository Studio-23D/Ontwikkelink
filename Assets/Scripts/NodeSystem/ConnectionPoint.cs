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

    public int height;

    public Action<ConnectionPoint> OnClickConnectionPoint;

    //public T data;

    public ConnectionPoint(Node node, ConnectionPointType type, GUIStyle style, int height, Action<ConnectionPoint> OnClickConnectionPoint)
    {
        this.node = node;
        this.type = type;
        this.style = style;
        this.height = height;
        this.OnClickConnectionPoint = OnClickConnectionPoint;

        //this.data = data;
        
        rect = new Rect(0, 0, 20f, 20f);
    }

    public void Draw()
    {
        rect.y = node.rect.y + (30 * height);

        switch(type)
        {
            case ConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8f;
                break;

            case ConnectionPointType.Out:
                rect.x = node.rect.x + node.rect.width - 8f;
                break;
        }

        if(GUI.Button(rect, "", style))
        {
            if(OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }
    }
}
