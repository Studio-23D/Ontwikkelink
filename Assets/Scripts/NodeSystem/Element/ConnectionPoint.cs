using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class ConnectionPoint : Element
    {
        private Node node;
        private Connection connection;

		public FieldInfo Value { get; }

		private Rect rect;

        public Action<ConnectionPoint> OnClickConnectionPoint;

        public ConnectionPoint(Node node, FieldInfo value)
        {
            this.node = node;

            this.Value = value;
            
            rect = new Rect();

            rect.width = 10;
            rect.height = 10;
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


