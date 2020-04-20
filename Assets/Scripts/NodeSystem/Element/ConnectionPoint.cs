using System;
using UnityEngine;

namespace NodeSystem
{
    public class ConnectionPoint : Element
    {
        private Node node;
        private Connection connection;
        private Type value;

        public Rect rect;

        public Action<ConnectionPoint> OnClickConnectionPoint;

        public ConnectionPoint(Node node, Vector2 position)
        {
            this.node = node;

            rect = new Rect(position.x, position.y, 10f, 20f);
        }

        public void Init()
        {
            
        }

        public override void Draw()
        {
            if (GUI.Button(rect, ""))
            {
                if (OnClickConnectionPoint != null)
                {
                    OnClickConnectionPoint(this);
                }
            }
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}


