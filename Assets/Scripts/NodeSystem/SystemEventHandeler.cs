using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
	public class SystemEventHandeler
	{
		private bool isDragging = false;
		private int dragCount = 0;
		public bool IsDragging => isDragging;
		public int DragCount => dragCount;


		public ConnectionPoint selectedPropertyPoint;
		public Vector2 MousePosition {
			get
			{
				return Event.current.mousePosition;
			}
		}

		public SystemEventHandeler()
		{
			OnElementHold += (Element element) =>
			{
				isDragging = true;
			};
			OnElementRelease += (Element element) =>
			{
				isDragging = false;
			};
		}


		public Action OnParrentChange = delegate { };
		public Action<Element> OnElementCreate = delegate { };
		public Action<Event> OnGui = delegate { };
		public Action<Element> OnElementClick = delegate { };
		public Action<Element> OnElementRelease = delegate { };
		public Action<Element> OnElementHold = delegate { };
		public Action<Element> OnElementRemove = delegate { };
	}
}
