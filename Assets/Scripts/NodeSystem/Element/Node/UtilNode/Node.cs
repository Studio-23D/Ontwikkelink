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
        protected string name;
        public bool isDragged;
        public bool isSelected;


        protected Rect nodeTopRect;
        protected Rect nodeMiddleRect;
        protected Rect nodeMiddleSecondRect;
        protected Rect nodeBottomRect;

        protected GUIStyle styleTop;
        protected GUIStyle styleMiddle;
        protected GUIStyle styleMiddleSecond;
        protected GUIStyle styleBottom;

        protected GUIStyle styleCenter;

        private List<ConnectionPoint> inputPoints;
		private List<ConnectionPoint> outputPoints;

		public Node()
		{
			inputPoints = new List<ConnectionPoint>();
			outputPoints = new List<ConnectionPoint>();

            styleTop = new GUIStyle();
            styleTop.normal.background = Resources.Load<Texture2D>("NodeSystem/node_1Top");
            styleTop.alignment = TextAnchor.MiddleCenter;
            styleTop.normal.textColor = Color.white;
            styleTop.font = (Font)Resources.Load("NodeSystem/bebas-neue-semiroundedNode");

            styleMiddle = new GUIStyle();
            styleMiddle.normal.background = Resources.Load<Texture2D>("NodeSystem/node_1Middle");

            styleMiddleSecond = new GUIStyle();
            styleMiddleSecond.normal.background = Resources.Load<Texture2D>("NodeSystem/node_1Middle2");

            styleBottom = new GUIStyle();
            styleBottom.normal.background = Resources.Load<Texture2D>("NodeSystem/node_1Bottom");

            styleCenter = new GUIStyle();
            styleCenter.alignment = TextAnchor.MiddleCenter;
        }

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
            base.Init(position, eventHandeler);
            rect.size = new Vector2(150, 200);

            rect = new Rect(position.x, position.y, 400, 450);

            nodeTopRect = new Rect(0, 0, 200, 25);
            nodeMiddleRect = new Rect(0, nodeTopRect.height, 200, 0);

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

            if (inputPoints.Count > outputPoints.Count)
            {
                nodeMiddleRect.height = 25 * inputPoints.Count;
            }
            else
            {
                nodeMiddleRect.height = 25 * outputPoints.Count;
            }
            
        }

		public virtual void AddConnectionPoint(FieldInfo field, ConnectionPointType pointType)
		{
			ConnectionPoint point = new ConnectionPoint(this, field, pointType);
            Vector2 position;
            Vector2 size = new Vector2(10, 10);
			switch (pointType)
			{
				case ConnectionPointType.In:
					inputPoints.Add(point);
                    position = new Vector2(10, point.Size.y + 20 * inputPoints.Count);
					break;
                default:
                    outputPoints.Add(point);
                    position = new Vector2(rect.width - point.Size.x - 20, point.Size.y + 20 * outputPoints.Count);
					break;
			}
            point.Init(position, this.eventHandeler);
		}

		public abstract void CalculateChange();

		public override void Draw()
		{
            GUI.BeginGroup(rect);

            GUI.Box(nodeTopRect, name, styleTop);

            GUI.Box(nodeMiddleRect, "", styleMiddle);

            List<ConnectionPoint> points = new List<ConnectionPoint>();
            points.AddRange(inputPoints);
            points.AddRange(outputPoints);

            foreach (ConnectionPoint point in points)
            {
                point.Draw();
            };
		}

		public override void Destroy()
		{
			throw new NotImplementedException();
		}


		public virtual void Drag(Vector2 position)
		{
			rect.position += position;
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