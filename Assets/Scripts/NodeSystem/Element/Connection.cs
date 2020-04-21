using System;
using UnityEditor;
using UnityEngine;

namespace NodeSystem {
	public class Connection : Element
	{
		private ConnectionPoint inPoint;
		private ConnectionPoint outPoint;

		public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint)
		{
			this.inPoint = inPoint;
			this.outPoint = outPoint;
		}

		public void SetValue()
		{
			outPoint.Value.SetValue(outPoint, inPoint.Value.GetValue(inPoint));
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
