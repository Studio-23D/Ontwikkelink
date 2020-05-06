using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{

    public class ConnectionPoint : Element
    {
        public Node node;
        private Connection connection;

		public FieldInfo Value { get; }

        public ConnectionPointType type;

        public override Rect Rect
        {
            get
            {
                Rect rect = this.rect;
                rect.position = node.Position + this.rect.position;
                return rect;
            }
        }
        GUIStyle style;

        public override Vector2 Position => Rect.position;

        public ConnectionPoint(Node node, FieldInfo value, ConnectionPointType type)
        {
            this.node = node;

            this.Value = value;
            this.type = type;
            
            rect = new Rect();
        }

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);
            rect.size = new Vector2(10,10);
            
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
            rect.width = 10;
            rect.height = 10;

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
            connection = null;
        }

        public override void Draw()
        {
            GUI.Box(rect, "", style);
            switch(type)
            {
                case ConnectionPointType.In:
                    GUI.Label(new Rect(base.Position.x + 15, base.Position.y - 5, 30, 20), Value.Name);
                    break;
                case ConnectionPointType.Out:
                    GUI.Label(new Rect(base.Position.x - 30, base.Position.y - 5, 30, 20), Value.Name);
                    break;
            }
            //temp
            connection?.SetValue();
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}