using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{

	public class ConnectionPoint : Element
	{
		public Connection Connection => connection;
		
		public override Vector2 Position => Rect.position;
        public Vector2 LocalPros => this.rect.position;
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

		private Connection connection;
        private GUIStyle style;
		private float pointSize = 40f;
		private float textWidth = 100f;

		public ConnectionPoint(Node node, FieldInfo value, ConnectionPointType type)
        {
            this.node = node;
            this.Value = value;
            this.type = type;

            rect = new Rect();
            rect.size = new Vector2(pointSize, pointSize);
        }

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);

            OnClick((Element element) =>
            {
                if (!(element is ConnectionPoint)) return;

                ConnectionPoint point = element as ConnectionPoint;

				switch (point.type)
                {
                    case ConnectionPointType.In:
						// Connects connection to input when connection has been made from a outpoint
						if (eventHandeler.selectedPropertyPoint != null && connection == null)
						{
							eventHandeler.selectedPropertyPoint.connection.Connect(point);
							connection.InPoint = point;
							connection.Init(Vector2.zero, eventHandeler);
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
						connection.OutPoint = point;
						connection.Init(Vector2.zero, eventHandeler);
						eventHandeler.selectedPropertyPoint = point;
						break;
				}
            });

            rect.width = pointSize;
            rect.height = pointSize;

            style = new GUIStyle();
            style.normal.background = Resources.Load<Texture2D>("NodeSystem/nodeDot");

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
            GUI.Box(rect, "", style);
            switch(type)
            {
                case ConnectionPointType.In:
                    GUI.Label(new Rect(base.Position.x + pointSize + 5, base.Position.y + pointSize / 4, textWidth, pointSize / 2), Value.Name);
                    break;
                case ConnectionPointType.Out:
                    GUI.Label(new Rect(base.Position.x - pointSize - 10, base.Position.y + pointSize / 4, textWidth, pointSize / 2), Value.Name);
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