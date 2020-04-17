using System;
using UnityEditor;
using UnityEngine;

namespace NodeSystem {
	public class Connection : Element
	{
		private ConnectionPoint inPoint;
		private ConnectionPoint outPoint;

		public void SetValue()
		{
			
		}

		public override void Destroy()
		{
			throw new NotImplementedException();
		}

		public override void Draw()
		{
			throw new NotImplementedException();
		}
	}
}
