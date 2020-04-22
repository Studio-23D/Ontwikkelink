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
        private Rect inputField;

		private Action onButtonDown = delegate { };
		public Vector2 MousePosition {
			get
			{
				return Event.current.mousePosition;
			}
		}

		public Action CheckHover = delegate { };

		public static Vector2 mousePosition => Event.current.mousePosition;
		public SystemEventHandeler(Rect inputField)
		{
            this.inputField = inputField;

			guiEventPairs = new Dictionary<EventType, Action>();

			guiEventPairs.Add(EventType.MouseDown, onButtonDown);
		}

		public void CheckInput()
		{
			EventType currentEvent = Event.current.type;
			CheckHover?.Invoke();
			if (!guiEventPairs.ContainsKey(currentEvent) || !inputField.Contains(MousePosition)) return;
			guiEventPairs[currentEvent]?.Invoke();
		}

		public void SubscribeTo(EventType eventType, Action action)
		{
			if (!guiEventPairs.ContainsKey(eventType)) return;
			guiEventPairs[eventType] += action;
		}

		public ConnectionPoint selectedPropertyPoint;

		public static Action<Element> OnElementCreate = (element) => { };
		public static Action<Element> OnElementClick = (element) => { };
		public static Action<Element> OnElementHover = (element) => { };
		public static Action<Element> OnElementRemove = (element) => { };
	}
}
