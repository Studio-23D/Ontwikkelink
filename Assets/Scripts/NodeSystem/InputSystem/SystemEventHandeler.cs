using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
	public class SystemEventHandler : MonoBehaviour
	{
		private InputState currentState;
		public InputState CurrentState
		{
			get => currentState;
			set {
				currentState?.OnStateLeave();
				currentState = value;
				currentState.OnStateEnter();
			}
		}

		public Vector2 MousePosition => Event.current.mousePosition;
		private Element selectedElement;
		public Element SelectedElement
		{
			get => selectedElement;
			set => selectedElement = value;
		}
		public bool IsSelectedElementThis<T>()
		{
			if (currentState == null) return false;
			if (currentState is T)
				return true;
			return false;
		}
		public T GetSelectedElement<T>()
		{
			if (selectedElement == null) return default;
			if (selectedElement is T)
				return (T)Convert.ChangeType(selectedElement, typeof(T));
			throw new Exception("Given datatype is not a variant of InputState");
		}

		public Action<Element> OnElementCreate = delegate { };
		public Action<Element> OnElementDestroy = delegate { };
		public Action<Element> OnElementClicked = delegate { };
		public Action<Element> OnElementHover = delegate { };
		public Action<Element> OnElementSelected = delegate { };
		public Action<InputTypes> OnInput = delegate { };

		private EventType prevouseEventType;

		public SystemEventHandler()
		{
			currentState = new Idle(this);
		}

		public bool IsCurrentStateThis<T>()
		{
			if (currentState == null) return default;
			if (currentState is T)
				return true;
			return false;
		}

		public T GetCurrentState<T>()
		{
			if (currentState == null) return default;
			if (currentState is T)
				return (T)Convert.ChangeType(currentState, typeof(T));
			throw new Exception("Given datatype is not a variant of InputState");
		}

		public void CheckInput()
		{
			EventType currentEvent = Event.current.type;
			switch (currentEvent)
			{
				case EventType.MouseDown:
					this.StartCoroutine(CheckIfHolding());
					break;
				case EventType.MouseUp:
					OnInput?.Invoke(InputTypes.Clicked);
					break;
			}

			prevouseEventType = currentEvent;
		}

		public IEnumerator CheckIfHolding()
		{
			yield return new WaitForSeconds(0.5f);
			if (prevouseEventType == EventType.MouseDown)
			{
				OnInput?.Invoke(InputTypes.Clicked);
			}
		}
	}
}
