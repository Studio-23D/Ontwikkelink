using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
	public class SystemEventHandeler
	{
		private Dictionary<EventType, Action> guiEventPairs;

		private Action onButtonDown = delegate { };
		public SystemEventHandeler()
		{
			guiEventPairs = new Dictionary<EventType, Action>();
			guiEventPairs.Add(EventType.MouseDown, onButtonDown);
		}

		public void CheckInput()
		{
			EventType currentEvent = Event.current.type;

			if (!guiEventPairs.ContainsKey(currentEvent)) return;
			guiEventPairs[currentEvent]?.Invoke();
		}

		public void SubscribeTo(EventType eventType, Action action)
		{
			if (!guiEventPairs.ContainsKey(eventType)) return;
			guiEventPairs[eventType] += action;
		}

		public static Action<Element> OnElementCreate = (element) => { };
		public static Action<Element> OnElementRemove = (element) => { };
	}
}
