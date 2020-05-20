using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
	public abstract class InputState
	{
		protected Dictionary<InputTypes, InputState> conditions = new Dictionary<InputTypes, InputState>();
		protected SystemEventHandler eventHandler;

		public InputState(SystemEventHandler eventHandler)
		{
			this.eventHandler = eventHandler;
		}

		public void ChangeState(InputTypes inputTypes)
		{
			if (conditions.ContainsKey(inputTypes))
				eventHandler.CurrentState = conditions[inputTypes];
		}

		public virtual void OnStateEnter()
		{
			eventHandler.OnElementClicked += this.OnElementClick;
			eventHandler.OnElementHover += this.OnElementHover;
			eventHandler.OnElementSelected += this.OnElementSelected;
		}
		public virtual void OnStateLeave()
		{
			eventHandler.OnElementClicked -= this.OnElementClick;
			eventHandler.OnElementHover -= this.OnElementHover;
			eventHandler.OnElementSelected -= this.OnElementSelected;
		}

		public virtual void OnElementHover(Element element) {

		}
		public virtual void OnElementClick(Element element) {

		}
		public virtual void OnElementSelected(Element element) {

		}
	}
}

