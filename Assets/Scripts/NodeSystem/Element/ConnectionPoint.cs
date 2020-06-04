using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{

	public class ConnectionPoint : Element
	{
		public Connection Connection => connection;
		public override Vector2 Position => rect.position;

		public override Rect Rect
		{
			get
			{
				Rect rect = this.rect;
				rect.position = node.Position + this.rect.position;
				return rect;
			}
		}

		public FieldInfo Value { get; }

		public ConnectionPointType type;
		public Node node;
        public int index;
        public float startOffset;
        public float offset;

        private Dictionary<string, Color32> primaireColors = new Dictionary<string, Color32>();

        private Color color;

		private Connection connection;
        private GUIStyle style;
		private float pointSize = 40f;
		private float textWidth = 100f;

		public ConnectionPoint(Node node, FieldInfo value, ConnectionPointType type, int index, float startOffset, float offset)
        {
            this.node = node;
            this.Value = value;
            this.type = type;
            this.index = index;
            this.startOffset = startOffset;
            this.offset = offset;

            rect = new Rect();
            rect.size = new Vector2(pointSize, pointSize);
        }

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);

            style = new GUIStyle();
            style.normal.background = Resources.Load<Texture2D>("NodeSystem/Overhaul/Node_punt");

            rect.width = pointSize;
            rect.height = pointSize;

            SetPosition();

            primaireColors.Add("color", new Color32(241, 242, 228, 255));
            primaireColors.Add("red", new Color32(241, 242, 228, 255));
            primaireColors.Add("blue", new Color32(241, 242, 228, 255));
            primaireColors.Add("green", new Color32(241, 242, 228, 255));
            primaireColors.Add("Pattern", new Color32(225, 98, 100, 255));

            primaireColors.Add("hair", new Color32(237, 180, 211, 255));
            primaireColors.Add("torso", new Color32(129, 181, 203, 255));
            primaireColors.Add("legs", new Color32(127, 134, 179, 255));
            primaireColors.Add("shoes", new Color32(236, 190, 103, 255));

            if (!primaireColors.ContainsKey(Value.Name))
            {
                this.color = Color.white;
            }
            else
            {
                this.color = primaireColors[Value.Name];
            }
        }

        public void SetPosition()
        {
            switch (type)
            {
                case ConnectionPointType.In:
                    base.Position = new Vector2(node.NodePosition.x - Size.x / 2, startOffset + index * (Size.x + offset));
                    break;
                case ConnectionPointType.Out:
                    base.Position = new Vector2(node.NodePosition.x + node.nodeRect.size.x - Size.x / 2, startOffset + index * (Size.x + offset));
                    break;
            }
        }

        private Connection CreateConnection()
        {
            return connection = new Connection();
        }

        public void OnConnection(Connection connection)
        {
            this.connection = connection;
        }

        public void Disconnect()
        {
			if (connection == null) return;

			if (connection.IsConnected)
			{
				connection.Destroy();
                node.OnChange -= node.CalculateChange;
                node.OnChange -= connection.SetValue;
			}
			connection = null;
        }

        public override void Draw()
        {
            GUI.color = color;
            
            GUI.Box(rect, "", style);

            GUI.color = Color.white;
        }

		public override void OnClickDown()
		{
			base.OnClickDown();

			switch (type)
			{
				case ConnectionPointType.In:
					// Connects connection to input when connection has been made from a outpoint
					if (eventHandeler.selectedPropertyPoint != null && connection == null)
					{
						eventHandeler.selectedPropertyPoint.connection.Connect(this);

						if (connection != null)
						{
							connection.InPoint = this;
						}
						return;
					}
					else
					{
						Disconnect();

						if (eventHandeler.selectedPropertyPoint != null)
						{
							if (eventHandeler.selectedPropertyPoint.connection != null)
							{
								eventHandeler.selectedPropertyPoint.connection.Destroy();
							}
						}
					}
					break;

				case ConnectionPointType.Out:
					CreateConnection();
					connection.OutPoint = this;
					connection.Init(Vector2.zero, eventHandeler);
					eventHandeler.selectedPropertyPoint = this;
					break;
			}
		}

		public override void Destroy()
        {
			if (connection != null)
			{
				connection.Destroy();
				connection = null;
			}

			base.Destroy();
		}
	}
}