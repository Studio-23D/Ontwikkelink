using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
	public abstract class Node : Element
	{

		public override void Init(Vector2 position)
		{
			
		}

		public virtual void AddConnectionPoint()
		{

		}

		public abstract void CalculateChange();

		public override void Draw()
		{
			throw new NotImplementedException();
		}

		public override void Destroy()
		{
			throw new NotImplementedException();
		}

		public virtual void Drag()
		{

		}
	}
}