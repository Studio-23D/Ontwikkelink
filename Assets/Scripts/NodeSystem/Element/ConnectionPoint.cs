using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{

    public class ConnectionPoint : MonoBehaviour
    {
        private Connection connection;

		public FieldInfo Value { get; }

        public ConnectionPointType type;

        SystemEventHandeler eventHandeler;

        public void Start()
        {
            /*OnClick((Element element) =>
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
            });*/
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
            connection = null;
        }
    }
}