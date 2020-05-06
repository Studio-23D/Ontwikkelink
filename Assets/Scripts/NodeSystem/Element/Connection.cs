using System;
using UnityEditor;
using UnityEngine;

namespace NodeSystem {
	public class Connection : Element
	{
		private ConnectionPoint inPoint;
		private ConnectionPoint outPoint;	

		private Type type;

		private bool isConnected = false;


		public ConnectionPoint InPoint
		{
			get => inPoint;
			set
			{
				inPoint = value;
				type = type ?? value.Value.FieldType;
			}
		}
		public ConnectionPoint OutPoint
		{
			get => outPoint;
			set
			{
				outPoint = value;
				type = type ?? value.Value.FieldType;
			}
		}

		public void SetValue()
		{
			if (!isConnected) return;
			inPoint.Value.SetValue(inPoint.node, outPoint.Value.GetValue(outPoint.node));
		}

		public override void Destroy()
		{
			base.Destroy();
			Disconect();
			inPoint = null;
			outPoint = null;
			eventHandeler.selectedPropertyPoint = null;
		}

		public void Connect(Element element)
		{
			if (!(element is ConnectionPoint)) return;
			ConnectionPoint point = element as ConnectionPoint;
			ConnectionPoint otherPoint = (inPoint != null) ? inPoint : outPoint;

			if (point.type == otherPoint.type || point.Value.FieldType != type) {
				Destroy();
				return;
			}

			switch (point.type)
			{
				case ConnectionPointType.In:
					inPoint = point;
					break;
				default:
					outPoint = point;
					break;
			}

			point.OnConnection(this);
			isConnected = true;
			eventHandeler.selectedPropertyPoint = null;
			this.SetValue();
			outPoint.node.OnChange += this.SetValue;
			outPoint.node.OnChange += inPoint.node.CalculateChange;
		}

		private void Disconect()
		{
			outPoint.node.OnChange -= this.SetValue;
			outPoint.node.OnChange -= inPoint.node.CalculateChange;
			inPoint?.Disconect();
			outPoint?.Disconect();
		}

		public override void Draw()
		{
			Vector2 positionA = (outPoint != null) ? outPoint.Position + outPoint.Size / new Vector2(2 ,2) : SystemEventHandeler.mousePosition; 
			Vector2 positionB = (inPoint != null) ? inPoint.Position + inPoint.Size / new Vector2(2, 2) : SystemEventHandeler.mousePosition;

			GuiLineRenderer.DrawLine(positionA, positionB, Color.black, 2);
		}
	}
}
