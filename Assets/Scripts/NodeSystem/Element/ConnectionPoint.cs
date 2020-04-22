using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class ConnectionPoint : Element
    {
        private Node node;
        private Connection connection;
        private FieldInfo value;

        private ConnectionPointType type;

        GUIStyle style;

        private Rect rect;

        public Action<ConnectionPoint> OnClickConnectionPoint;

        public ConnectionPoint(Node node, FieldInfo value, ConnectionPointType type)
        {
            this.node = node;

            this.value = value;
            this.type = type;
            
            rect = new Rect();

            rect.width = 10;
            rect.height = 10;

            style = new GUIStyle();
            style.normal.background = Resources.Load<Texture2D>("NodeSystem/nodeDot");
        }

        public override void Init(Vector2 position)
        {
            base.Init(position);

            rect.position = position;
        }

        public Vector2 RectSize
        {
            get
            {
                return rect.size;
            }
        }

        public override void Draw()
        {
            if (GUI.Button(rect, "", style))
            {
                if (OnClickConnectionPoint != null)
                {

                }
            }
            switch(type)
            {
                case ConnectionPointType.In:
                    GUI.Label(new Rect(position.x + 15, position.y - 5, 30, 20), value.Name);
                    break;
                case ConnectionPointType.Out:
                    GUI.Label(new Rect(position.x - 30, position.y - 5, 30, 20), value.Name);
                    break;
            }
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}


