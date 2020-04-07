using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemEventHandeler
{
    private Dictionary<EventType, Action> eventPairs;
    public SystemEventHandeler()
    {
        eventPairs = new Dictionary<EventType, Action>();
        eventPairs.Add(EventType.MouseDown, onButtonDown);
    }

    private void OnGUI()
    {
        Event currentEvent = Event.current;

        eventPairs[currentEvent.type]?.Invoke();
    }

    public static Action onButtonDown = () => { };

    public static Action onInit = ()=> { };
    public static Action onNodeAdded = () => { };
    public static Action onRemoveNode = () => { };
    public static Action onGui = () => { };
}
