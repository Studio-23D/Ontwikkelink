using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
	public abstract class Node : Element
	{
		private List<ConnectionPoint> inputPoints;
		private List<ConnectionPoint> outPoints;

		public Node()
		{
			inputPoints = new List<ConnectionPoint>();
			outPoints = new List<ConnectionPoint>();

			FieldInfo[] objectFields = this.GetType().GetFields();
			foreach (FieldInfo field in objectFields)
			{
				var inputAttribute = Attribute.GetCustomAttribute(field, typeof(InputProppertyAttribute));
				var outputAttribute = Attribute.GetCustomAttribute(field, typeof(OutputProppertyAttribute));

				if (inputAttribute != null)
				{
					AddConnectionPoint(field, ConnectionPointType.In);
				} else if (outputAttribute != null) {
					AddConnectionPoint(field, ConnectionPointType.Out);
				}
				
			}
		}

		public override void Init(Vector2 position)
		{
			
		}

		public virtual void AddConnectionPoint(FieldInfo field, ConnectionPointType pointType)
		{
			ConnectionPoint point = new ConnectionPoint(field);
			switch (pointType)
			{
				case ConnectionPointType.In:
					inputPoints.Add(point);
					break;
				default:
					outPoints.Add(point);
					break;
			}
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

		public virtual void Drag(Vector2 position)
		{
			this.position = position;
		}
	}
}