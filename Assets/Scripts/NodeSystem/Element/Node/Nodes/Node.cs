using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
	public abstract class Node : Element
	{
        protected string name;
        protected Rect rect;
        public bool isDragged;
        public bool isSelected;

        protected List<ConnectionPoint> inputs;
        protected List<ConnectionPoint> outputs;
        
		public override void Init(Vector2 position)
		{
            base.Init(position);

            rect = new Rect(position.x, position.y, 150, 200);

            inputs = new List<ConnectionPoint>();
            outputs = new List<ConnectionPoint>();
        }

		public virtual void AddConnectionPoint()
		{
            inputs.Add(new ConnectionPoint(this, new Vector2(rect.x - 10 + 8f, rect.y + (rect.height * 0.5f) - 20 * 0.5f)));
            outputs.Add(new ConnectionPoint(this, new Vector2(rect.x + 10 - 8f, rect.y + (rect.height * 0.5f) - 20 * 0.5f)));
        }

		public abstract void CalculateChange();

		public override void Draw()
		{
            GUI.BeginGroup(rect);
            GUI.Box(new Rect(0, 0, 150, 200), name);

            foreach(ConnectionPoint input in inputs)
            {
                input.Draw();
            };

            foreach (ConnectionPoint output in outputs)
            {
                output.Draw();
            };

            GUI.EndGroup();
		}

		public override void Destroy()
		{
			throw new NotImplementedException();
		}

		public virtual void Drag(Vector2 delta)
		{
            rect.position += delta;
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