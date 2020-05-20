using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
	public abstract class InputState
	{
		protected Dictionary<Condition, InputState> conditions = new Dictionary<Condition, InputState>();
		protected SystemEventHandler eventHandler;

		public InputState(SystemEventHandler eventHandler)
		{
			this.eventHandler = eventHandler;
		}

		public void CheckState(Condition condition)
		{
			if (conditions.ContainsKey(condition))
				eventHandler.CurrentState = conditions[condition];
		}

		public abstract void OnStateEnter();
		public abstract void OnStateLeave();

		public virtual void OnElementHover(Element element) {

		}
		public virtual void OnElementClick(Element element) {

		}
		public virtual void OnElementSelected(Element element) {

		}
	}
}

