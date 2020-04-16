using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
	public class Node : Element
	{

		public virtual void Init(Rect rect)
		{
			this.rect = rect;
		}

		public override void Draw()
		{
			GUIStyle guiStyle = new GUIStyle();
			
			GUI.Box(this.rect, "", guiStyle);
		}

		public virtual void AddConnectionPoint()
		{

		}

		public virtual void Drag()
		{

		}
	}
}


/*
 * #region PUBLIC_STRUCTS

		public struct Styles
		{
			public GUIStyle main;
			public GUIStyle defaultStyle;
			public GUIStyle selected;

			public GUIStyle top;
			public GUIStyle mid;
			public GUIStyle bot;

			public GUIStyle pointStyle;
		}

		public struct Actions
		{
			public Action<Node> OnRemoveNode;
			public Action<ConnectionPoint> OnClickConnectionPoint;
		}

		[Serializable]
		public struct Sections
		{
			[HideInInspector] public Rect mainRect;

			[Tooltip("To set the section height purpose only")]
			public float topHeight;
			public Texture2D topBackground;
			// Used for calculations
			[HideInInspector] public Rect topRect;

			[Tooltip("To set the section height purpose only")]
			public float midHeight;
			public Texture2D midBackground;
			// Used for calculations
			[HideInInspector] public Rect midRect;

			[Tooltip("To set the section height purpose only")]
			public float botHeight;
			public Texture2D botBackground;
			// Used for calculations
			[HideInInspector] public Rect botRect;
		}

		#endregion



		#region PUBLIC_MEMBERS

		public Styles styles;
		public Sections sections;
		public Actions actions;
		public string title;
		public bool isDragged;
		public bool isSelected;

		public List<ConnectionPoint> inPoints = new List<ConnectionPoint>();
		public List<ConnectionPoint> outPoints = new List<ConnectionPoint>();

		private int inPointsAmount = 0;
		private int outPointsAmount = 0;

		#endregion



		#region PUBLIC_METHODS

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="position"></param>
		/// <param name="width"></param>
		/// <param name="sections"></param>
		/// <param name="style"></param>
		/// <param name="actions"></param>
		public Node(Vector2 position, float width, Sections sections, Styles style, Actions actions)
		{
			this.sections.mainRect = new Rect(position.x, position.y, width, sections.topHeight + sections.midHeight + sections.botHeight);

			this.sections.topRect = new Rect(position.x, position.y, width, sections.topHeight);
			this.sections.midRect = new Rect(position.x, position.y + sections.topHeight, width, sections.midHeight);
			this.sections.botRect = new Rect(position.x, position.y + sections.topHeight + sections.midHeight, width, sections.botHeight);

			this.styles.main = style.main;
			this.styles.defaultStyle = style.main;
			this.styles.selected = style.selected;

			this.styles.top = style.top;
			this.styles.mid = style.mid;
			this.styles.bot = style.bot;

			this.actions.OnRemoveNode = actions.OnRemoveNode;
			this.actions.OnClickConnectionPoint = actions.OnClickConnectionPoint;

			SetConnectionPoints();
			SetHeight(inPointsAmount, outPointsAmount);
		}

		protected void AddConnectionPoints(Styles style)
		{
			for (int i = 0; i < inPointsAmount; i++)
			{
				float pointHeight = (i + 1) * (sections.midRect.height / (inPointsAmount + 1));

				ConnectionPoint input = new ConnectionPoint(this, ConnectionPointType.In, style.pointStyle, pointHeight, actions.OnClickConnectionPoint);

				this.inPoints.Add(input);
			}

			for (int i = 0; i < outPointsAmount; i++)
			{
				float pointHeight = (i + 1) * (sections.midRect.height / (outPointsAmount + 1));

				ConnectionPoint output = new ConnectionPoint(this, ConnectionPointType.Out, style.pointStyle, pointHeight, actions.OnClickConnectionPoint);

				this.outPoints.Add(output);
			}
		}

		public void CalculateChange()
		{

		}

		public void Drag(Vector2 delta)
		{
			sections.mainRect.position += delta;
			sections.topRect.position += delta;
			sections.midRect.position += delta;
			sections.botRect.position += delta;
		}

		public override void Draw()
		{
			
		}

		/// <summary>
		/// Processes the player's interaction with the node
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public bool ProcessEvents(Event e)
		{
			switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 0)
					{
						if (sections.mainRect.Contains(e.mousePosition))
						{
							isDragged = true;
							GUI.changed = true;
							isSelected = true;
							styles.main = styles.selected;
						}
						else
						{
							GUI.changed = true;
							isSelected = false;
							styles.main = styles.defaultStyle;
						}
					}

					if (e.button == 1 && isSelected && sections.mainRect.Contains(e.mousePosition))
					{
						ProcessContextMenu();
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

		public void OnClickRemoveNode()
		{
			if (actions.OnRemoveNode != null)
			{
				actions.OnRemoveNode(this);
			}
		}

		public void SetHeight(int insAmount, int outsAmount)
		{
			if (insAmount > outsAmount)
			{
				sections.midRect.height *= insAmount;
			}
			else
			{
				sections.midRect.height *= outsAmount;
			}

			this.sections.botRect.position = new Vector2(this.sections.botRect.position.x, this.sections.botRect.position.y + this.sections.midRect.height - this.sections.botRect.height);
		}

		#endregion



		#region PRIVATE_METHODS

		private FieldInfo[] GetObjectFields<T>(T Object)
		{
			return Object.GetType().GetFields();
		}

		private void SetConnectionPoints()
		{
			foreach (FieldInfo field in GetObjectFields(this))
			{
				var input = Attribute.GetCustomAttribute(field, typeof(InputProppertyAttribute));
				var output = Attribute.GetCustomAttribute(field, typeof(OutputProppertyAttribute));

				if (input != null)
				{
					Type type = input.GetType();

					inPointsAmount++;
				}

				if (output != null)
				{
					Type type = output.GetType();

					outPointsAmount++;
				}
			}
		}

		private void ProcessContextMenu()
		{
			GenericMenu genericMenu = new GenericMenu();
			genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
			genericMenu.ShowAsContext();
		}

		#endregion
	}

	*/