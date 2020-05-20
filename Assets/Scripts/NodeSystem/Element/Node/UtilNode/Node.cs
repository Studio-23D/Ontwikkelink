using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public enum ConnectionPointType { In, Out }

namespace NodeSystem
{
	public abstract class Node : Element
	{
        public bool isDragged;
        public bool isSelected;

        protected string name;

        protected Dictionary<string, Rect> areas = new Dictionary<string, Rect>();

        private List<ConnectionPoint> inputPoints = new List<ConnectionPoint>();
        private List<ConnectionPoint> outputPoints = new List<ConnectionPoint>();

        private float connectionPointOffset = 5;

        protected GUIStyle styleTopArea;
        protected GUIStyle styleMiddleArea;
        protected GUIStyle styleBottomArea;

        public Action OnChange = delegate { };

		public Node()
		{
            inputPoints = new List<ConnectionPoint>();
            outputPoints = new List<ConnectionPoint>();

            styleTopArea = new GUIStyle();
            styleTopArea.normal.background = Resources.Load<Texture2D>("NodeSystem/Node/Normal/node_top");

            styleMiddleArea = new GUIStyle();
            styleMiddleArea.normal.background = Resources.Load<Texture2D>("NodeSystem/Node/Normal/node_middle");
            //stylemiddleArea.font = (Font)Resources.Load("NodeSystem/bebas-neue-semiroundedNode");

            styleBottomArea = new GUIStyle();
            styleBottomArea.normal.background = Resources.Load<Texture2D>("NodeSystem/Node/Normal/node_bottom");
        }

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
            base.Init(position, eventHandeler);

            areas.Add("top", new Rect(0, 0, 160, 20));
            areas.Add("middle", new Rect(0, areas["top"].height, 160, 150));
            areas.Add("bottom", new Rect(0, areas["top"].height + areas["middle"].height, 160, 20));

            Size = new Vector2(160, 190);

            FieldInfo[] objectFields = this.GetType().GetFields();
            foreach (FieldInfo field in objectFields)
            {
                var inputAttribute = Attribute.GetCustomAttribute(field, typeof(InputProppertyAttribute));
                var outputAttribute = Attribute.GetCustomAttribute(field, typeof(OutputProppertyAttribute));

                if (inputAttribute != null)
                {
                    AddConnectionPoint(field, ConnectionPointType.In);
                }
                else if (outputAttribute != null)
                {
                    AddConnectionPoint(field, ConnectionPointType.Out);
                }
            }
            PositionConnectionPoints(inputPoints, areas["middle"].height / 2 + areas["top"].height);
        }

		public override void Draw()
		{
            GUI.BeginGroup(rect);

            GUI.Box(areas["top"], "", styleTopArea);
            GUI.Box(areas["middle"], "", styleMiddleArea);
            GUI.Box(areas["bottom"], "", styleBottomArea);

            foreach (ConnectionPoint point in inputPoints)
            {
                point.Draw();
            };
            foreach (ConnectionPoint point in outputPoints)
            {
                point.Draw();
            };
        }

        public virtual void CalculateChange()
        {
            OnChange?.Invoke();
        }

        public override void Destroy()
		{
			throw new NotImplementedException();
		}

		public virtual void Drag(Vector2 position)
		{
#if UNITY_EDITOR
			rect.position += new Vector2(position.x, position.y);
#else
			rect.position += new Vector2(position.x, -position.y);
#endif
		}

        public virtual void AddConnectionPoint(FieldInfo field, ConnectionPointType pointType)
        {
            ConnectionPoint point = new ConnectionPoint(this, field, pointType);

            switch (pointType)
            {
                case ConnectionPointType.In:
                    inputPoints.Add(point);
                    break;
                case ConnectionPointType.Out:
                    outputPoints.Add(point);
                    break;
            }
            point.Init(new Vector2(0, 0), this.eventHandeler);
        }

        public virtual void PositionConnectionPoints(List<ConnectionPoint> points, float positionY)
        {
            ConnectionPointType type = points[0].type;
            float totalSizePoints = (points[0].Size.y + connectionPointOffset) * points.Count;

            for(int i = 0; i < points.Count; i++)
            {
                switch (type)
                {
                    case ConnectionPointType.In:
                        points[i].Position = new Vector2(0, i * (points[i].Size.y + connectionPointOffset) + positionY - totalSizePoints / 2);
                        break;
                    case ConnectionPointType.Out:
                        points[i].Position = new Vector2(areas["middle"].width, i * (points[i].Size.y + connectionPointOffset) + positionY - totalSizePoints / 2);
                        break;
                }

            }
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            GUI.changed = true;
                            isSelected = true;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                        }
                    }

                    if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                    {
                        e.Use();
                    }
                    break;

                case EventType.MouseUp:
                    isDragged = false;
                    break;

                case EventType.MouseDrag:
                    //if ((e.button == 0 || e.pointerType == PointerType.Touch) && isDragged)
					if (e.button == 0 && isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}