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
				OnElementClicked -= currentState.OnElementClick;

				currentState = value;
				currentState.OnStateEnter();
			}
		}

		public Vector2 MousePosition => Event.current.mousePosition;
		public Element selectedElement;

		public Action<Element> OnElementCreate = delegate { };
		public Action<Element> OnElementDestroy = delegate { };
		public Action<Element> OnElementClicked = delegate { };
		public Action<Element> OnElementHover = delegate { };
		public Action<Element> OnElementSelected = delegate { };
		public Action OnClick = delegate { };
		public Action OnHover = delegate { };
		public Action OnHold = delegate { };

		private EventType prevouseEventType;

		public SystemEventHandler()
		{
			currentState = new Movement(this);
		}

		public bool IsCurrentStateThis<T>()
		{
			if (currentState is T)
				return true;
			return false;
		}

		public T GetCurrentState<T>()
		{
			return (T)Convert.ChangeType(currentState, typeof(T));
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
					OnClick?.Invoke();
					break;
			}

			prevouseEventType = currentEvent;
		}

		public IEnumerator CheckIfHolding()
		{
			yield return new WaitForSeconds(0.5f);
			if (prevouseEventType == EventType.MouseDown)
			{
				OnHold?.Invoke();
			}
		}
	}
}
