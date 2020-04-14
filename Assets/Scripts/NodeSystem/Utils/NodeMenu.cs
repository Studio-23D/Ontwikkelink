using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeMenu 
{
    GenericMenu genericMenu;
    SystemEventHandeler eventHandeler;

    private List<MenuEntry> menuEntries;

    public NodeMenu(SystemEventHandeler eventHandeler)
    {
        this.eventHandeler = eventHandeler; 
        menuEntries = new List<MenuEntry>();
        this.Init();
    }

    private void Init()
    {

        eventHandeler.SubscribeTo(EventType.MouseDown, () =>
        {
            genericMenu = new GenericMenu();
            menuEntries.ForEach(entry => genericMenu.AddItem(new GUIContent(entry.name), false, () => entry.OnClick.Invoke()));
            genericMenu.ShowAsContext();
        });
    }

    public void CreateMenuEntry(String node, Action OnClick)
    {
        MenuEntry menuEntry = new MenuEntry
        {
            name = node,
            OnClick = OnClick
        };

        menuEntries.Add(menuEntry);
        
    }
}

struct MenuEntry {
    public string name;
    public Action OnClick;
}