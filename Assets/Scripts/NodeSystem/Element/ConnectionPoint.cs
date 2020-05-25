using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{

    public class ConnectionPoint : Element
	{
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
		private float size = 40;

        public ConnectionPoint(Node node, FieldInfo value, ConnectionPointType type)
        {
            this.node = node;
            this.Value = value;
            this.type = type;
            

            rect = new Rect();
            rect.size = new Vector2(size, size);
        }

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);

            OnClick((Element element) =>
            {
                if (!(element is ConnectionPoint)) return;
                ConnectionPoint point = element as ConnectionPoint;
                if (eventHandeler.selectedPropertyPoint != null)
                {
                    eventHandeler.selectedPropertyPoint.connection.Connect(point);
                    return;
                }
                CreateConnection();

                switch (point.type)
                {
                    case ConnectionPointType.In:
                        connection.InPoint = point;
                        break;
                    default:
                        connection.OutPoint = point;
                        break;
                }

                connection.Init(Vector2.zero, eventHandeler);
                eventHandeler.selectedPropertyPoint = point;
            });

            rect.width = size;
            rect.height = size;

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

        public void Disconect()
        {
            if (connection.IsConnected)
            {
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
                    GUI.Label(new Rect(base.Position.x + size + 5, base.Position.y + size / 4, 30, 20), Value.Name);
                    break;
                case ConnectionPointType.Out:
                    GUI.Label(new Rect(base.Position.x - size - 10, base.Position.y + size / 4, 30, 20), Value.Name);
                    break;
            }
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}