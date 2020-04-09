using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SystemEventHandeler
{
    private Dictionary<EventType, Action> eventPairs;
    public SystemEventHandeler()
    {
        eventPairs = new Dictionary<EventType, Action>();
        eventPairs.Add(EventType.MouseDown, onButtonDown);
    }

    public void CheckInput()
    {
        Event currentEvent = Event.current;

        if (!eventPairs.ContainsKey(currentEvent.type))
            return;

        eventPairs[currentEvent.type]?.Invoke();
    }

    public static Action onButtonDown = () => { NodeMenu nodeMenu =  new NodeMenu(Event.current.mousePosition); nodeMenu.Init(); };

    public static Action onInit = ()=> { };
    public static Action<Node> onNodeAdd = (node) => { };
    public static Action onRemoveNode = () => { };
    public static Action onGui = () => { };
}
