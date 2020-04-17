using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SystemEventHandeler
{
    private Dictionary<EventType, Action> eventPairs;

    public Action onButtonDown = delegate { };
    public SystemEventHandeler()
    {
        eventPairs = new Dictionary<EventType, Action>();
        eventPairs.Add(EventType.MouseDown, onButtonDown);
    }

    public void CheckInput()
    {
        Event currentEvent = Event.current;

        if (!eventPairs.ContainsKey(currentEvent.type) || currentEvent.button == 0)
            return;
    
        eventPairs[currentEvent.type]?.Invoke();
    }

    public void SubscribeTo(EventType eventType, Action action)
    {
        if (!eventPairs.ContainsKey(eventType)) return;
        eventPairs[eventType] += action;
    }

    public Action onInit = ()=> { };
    public Action<Node> onNodeAdd = (node) => { };
    public Action onRemoveNode = () => { };
    public Action onGui = () => { };
}
