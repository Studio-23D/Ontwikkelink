using System;
using UnityEditor;
using UnityEngine;

public class NodeMenu 
{
    GenericMenu genericMenu;

    public Vector2 position;

    public NodeMenu(Vector2 mousePosition)
    {
        this.position = mousePosition;
    }

    public void Init()
    {
        genericMenu = new GenericMenu();

        genericMenu.ShowAsContext();
    }

    public void CreateMenuEntry(String node, Action OnClick)
    {
        genericMenu.AddItem(new GUIContent(node), false, () => OnClick.Invoke());
    }
}
