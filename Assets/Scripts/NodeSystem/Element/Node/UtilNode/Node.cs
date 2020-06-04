using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum ConnectionPointType { In, Out }

namespace NodeSystem
{
	public abstract class Node : Element
	{
        public Rect nodeRect;

        public virtual Vector2 NodePosition
        {
            get => new Vector2(50, 0);
        }
        public Vector2 NodeSize
        {
            get => new Vector2(Size.x - 100, Size.y);
        }

        public float topBottomSize;

		public List<ConnectionPoint> ConnectionPoints
		{
			get
			{
				List<ConnectionPoint> connectionPoints = new List<ConnectionPoint>();

				connectionPoints = inputPoints.Union<ConnectionPoint>(outputPoints).ToList<ConnectionPoint>();

				return connectionPoints;
			}
		}
		public List<ConnectionPoint> InputPoints => inputPoints;
		public List<ConnectionPoint> OutputPoints => outputPoints;

        protected float connectionPointOffset;
        protected float connectionPointStartOffset;

        public Vector3 StartPosition => startPosition;

        protected string name;
        protected Color32 primaireColor = Color.white;
        protected Color32 secondaireColor = Color.white;

        protected float height;
        protected float width;

        protected Texture2D nodeImage;

        protected Texture2D divideLine;

        protected float elementY = 0;

        protected List<Rect> nodeAreas;

        protected GUIStyle styleTopArea;
        protected GUIStyle styleMiddleArea;
        protected GUIStyle styleBottomArea;

        protected GUIStyle noStyle = new GUIStyle();

        protected Rect iconRect;
        protected Rect titleRect;
        protected Rect divideLineRect;

        protected GUIStyle styleText;

        private List<ConnectionPoint> inputPoints;
		private List<ConnectionPoint> outputPoints;

        public Action OnChange = delegate { };

		public Node()
		{
			inputPoints = new List<ConnectionPoint>();
			outputPoints = new List<ConnectionPoint>();

            nodeAreas = new List<Rect>();

            topBottomSize = 35;

            styleTopArea = new GUIStyle();
            styleTopArea.normal.background = Resources.Load<Texture2D>("NodeSystem/Overhaul/node_top");

            styleMiddleArea = new GUIStyle();
            styleMiddleArea.normal.background = Resources.Load<Texture2D>("NodeSystem/Overhaul/node_middle");

            styleBottomArea = new GUIStyle();
            styleBottomArea.normal.background = Resources.Load<Texture2D>("NodeSystem/Overhaul/node_bottom");

            styleText = new GUIStyle();
            styleText.alignment = TextAnchor.MiddleCenter;
            styleText.normal.textColor = Color.white;
            styleText.font = (Font)Resources.Load("NodeSystem/bebas-neue-semiroundedNode");
            styleText.fontSize = 38;

            divideLine = Resources.Load<Texture2D>("NodeSystem/Overhaul/node_scheiding_lijn"); 
        }

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
            base.Init(position, eventHandeler);

            nodeAreas.Add(new Rect(NodePosition.x, NodePosition.y, width, topBottomSize));
            nodeAreas.Add(new Rect(NodePosition.x, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, width, height - topBottomSize * 2));
            nodeAreas.Add(new Rect(NodePosition.x, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, width, topBottomSize));

            float nodeHeight = nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height;
            rect.size = new Vector2(width + 100, nodeHeight);
            nodeRect.size = new Vector2(width, nodeHeight);

            elementY = 80;
            iconRect = new Rect(NodePosition.x + NodeSize.x / 2 - 30, NodePosition.y + 10, 60, 60);

            if (!nodeImage) elementY = 10;

            titleRect = new Rect(NodePosition.x, elementY, width, 40);
            elementY += 40;

            divideLineRect = new Rect(NodePosition.x + NodeSize.x / 2 - 70, NodePosition.y + elementY, 140, 20);

            elementY += 20;

            int index = 0;
            FieldInfo[] objectFields = this.GetType().GetFields();
            foreach (FieldInfo field in objectFields)
            {
                if (Attribute.IsDefined(field, typeof(InputProppertyAttribute)))
                {
                    if (inputPoints.Count < index) index = 0;
                    AddConnectionPoint(field, ConnectionPointType.In, inputPoints, index);
                }
                else if (Attribute.IsDefined(field, typeof(OutputProppertyAttribute)))
                {
                    if (outputPoints.Count < index) index = 0;
                    AddConnectionPoint(field, ConnectionPointType.Out, outputPoints, index);
                }
                index++;
            }
            List<ConnectionPoint> higherList = inputPoints.Count > outputPoints.Count ? inputPoints : outputPoints;
        }

		public virtual void AddConnectionPoint(FieldInfo field, ConnectionPointType pointType, List<ConnectionPoint> list, int index)
		{
			ConnectionPoint point = new ConnectionPoint(this, field, pointType, index, connectionPointStartOffset, connectionPointOffset);
            Vector2 position = new Vector2();

			switch (pointType)
			{
				case ConnectionPointType.In:
					inputPoints.Add(point);
					break;
                case ConnectionPointType.Out:
                    outputPoints.Add(point);
					break;
			}

            point.Init(position, eventHandeler);
		}

        public virtual void CalculateChange()
        {
            OnChange?.Invoke();
        }

		public override void Draw()
		{
            GUI.BeginGroup(rect);

            GUI.color = primaireColor;
            GUI.Box(nodeAreas[0], "", styleTopArea);
            GUI.Box(nodeAreas[1], "", styleMiddleArea);
            GUI.Box(nodeAreas[2], "", styleBottomArea);

            GUI.color = Color.white;

            List<ConnectionPoint> points = new List<ConnectionPoint>();
            points.AddRange(inputPoints);
            points.AddRange(outputPoints);

            foreach (ConnectionPoint point in points)
            {
                point.Draw();
            };

            GUI.DrawTexture(iconRect, nodeImage, ScaleMode.ScaleToFit, true);

            GUI.color = secondaireColor;
            GUI.Label(titleRect, name, styleText);

            GUI.color = primaireColor;
            GUI.DrawTexture(divideLineRect, divideLine, ScaleMode.ScaleToFit, true);

            GUI.color = secondaireColor;
        }

		public override void Destroy()
		{
			foreach (ConnectionPoint connectionPoint in ConnectionPoints)
			{
				connectionPoint.Destroy();
				ConnectionPoints.Remove(connectionPoint);
			}

			if (eventHandeler.selectedPropertyPoint != null)
			{
				eventHandeler.selectedPropertyPoint.Destroy();
				ConnectionPoints.Remove(eventHandeler.selectedPropertyPoint);
			}

			base.Destroy();
		}

        public void ResetPosition()
        {
            rect.position = startPosition;
        }

        public void SaveStartPosition()
        {
            startPosition = this.Position;
        }

		public void EnableFieldDrag()
		{
			IsFieldDragged(true);
		}

		public void DisableFieldDrag()
		{
			IsFieldDragged(false);
		}

		public override void OnClickDown()
        {
            base.OnClickDown();
        }

        public override void OnHold(Vector2 position)
		{
            base.OnHold(position);

			if (SystemInfo.deviceType == DeviceType.Desktop)
			{
				Position += new Vector2(position.x, position.y);
			}
			else
			{
                Position += new Vector2(position.x, -position.y);
			}
		}
    }
}