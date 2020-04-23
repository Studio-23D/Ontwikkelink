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

        protected List<Rect> nodeAreas;

        protected GUIStyle styleTitleArea;
        protected GUIStyle styleConnectionPointsArea;
        protected GUIStyle styleExtraArea;
        protected GUIStyle styleBottomArea;

        protected GUIStyle styleCenter;

        private List<ConnectionPoint> inputPoints;
		private List<ConnectionPoint> outputPoints;


		public Node()
		{
			inputPoints = new List<ConnectionPoint>();
			outputPoints = new List<ConnectionPoint>();

            nodeAreas = new List<Rect>();

            styleTitleArea = new GUIStyle();
            styleTitleArea.normal.background = Resources.Load<Texture2D>("NodeSystem/node_1Top");
            styleTitleArea.alignment = TextAnchor.MiddleCenter;
            styleTitleArea.normal.textColor = Color.white;
            styleTitleArea.font = (Font)Resources.Load("NodeSystem/bebas-neue-semiroundedNode");

            styleConnectionPointsArea = new GUIStyle();
            styleConnectionPointsArea.normal.background = Resources.Load<Texture2D>("NodeSystem/node_1Middle");

            styleExtraArea = new GUIStyle();
            styleExtraArea.normal.background = Resources.Load<Texture2D>("NodeSystem/node_1Middle2");

            styleBottomArea = new GUIStyle();
            styleBottomArea.normal.background = Resources.Load<Texture2D>("NodeSystem/node_1Bottom");

            styleCenter = new GUIStyle();
            styleCenter.alignment = TextAnchor.MiddleCenter;
        }

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
            base.Init(position, eventHandeler);

            //rect.size = new Vector2(200, 0);

            nodeAreas.Add(new Rect(0, 0, 200, 25));

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
            float pointAreaHeight = 0;
            if (inputPoints.Count > outputPoints.Count)
            {
                pointAreaHeight = 25 * inputPoints.Count;
            }
            else
            {
                pointAreaHeight = 25 * outputPoints.Count;
            }
            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count-1].y + nodeAreas[nodeAreas.Count - 1].height, 200, pointAreaHeight));
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
                    position = new Vector2(10, ((point.Size.y + nodeAreas[nodeAreas.Count-1].height) * inputPoints.Count) + 10);
					break;
                default:
                    outputPoints.Add(point);
                    position = new Vector2(200 - point.Size.x - 20, ((point.Size.y + nodeAreas[nodeAreas.Count-1].height) * outputPoints.Count) + 10);
					break;
			}
            point.Init(position, this.eventHandeler);
		}

		public abstract void CalculateChange();

		public override void Draw()
		{
            GUI.BeginGroup(rect);

            GUI.Box(nodeAreas[0], name, styleTitleArea);

            GUI.Box(nodeAreas[1], "", styleConnectionPointsArea);

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