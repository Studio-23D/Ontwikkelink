using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public abstract class Element
    {
        public bool IsBeingDragged => isBeingDragged;
        public bool IsFieldDragged(bool drag) => isFieldDragged = drag;
        public bool IsSelected => isSelected;
		public Vector2 SetStartPosition(Vector2 startPosition) => this.startPosition = startPosition;
		public Vector2 GetStartPosition => startPosition;
        public int DrawOrder => drawOrder;
        public virtual Rect MainRect => mainRect;

        public virtual Vector2 MainPosition
        {
            get => mainRect.position;
            protected set => mainRect.position = value;
        }
        public Vector2 MainSize
        {
            get => mainRect.size;
            protected set => mainRect.size = value;
        }

        protected Vector2 startPosition;
        protected Rect mainRect = new Rect();
        protected SystemEventHandeler eventHandeler;
        protected bool isBeingDragged;

        private bool isFieldDragged;
        private bool isSelected;
		private int drawOrder = 0;
        private Dictionary<EventType, Action<Event>> eventTypes = new Dictionary<EventType, Action<Event>>();

        public virtual void Init(Vector2 position,  SystemEventHandeler eventHandeler)
        {
            drawOrder = 0;

			startPosition = position;
			Vector2 size = new Vector2(100, 100);
            mainRect = new Rect(position, size);

            eventHandeler.OnElementCreate?.Invoke(this);
            eventHandeler.OnGui += ProcessEvents;

            eventTypes.Add(EventType.MouseDown, (Event e) =>
            {
                if (e.button == 0)
                {
                    if (MainRect.Contains(e.mousePosition))
                    {
                        OnClickDown();
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                    }
                }
            });

            eventTypes.Add(EventType.MouseUp, (Event e) =>
            {
                OnClickUp();
            });

            eventTypes.Add(EventType.MouseDrag, (Event e) =>
            {
                if (MainRect.Contains(e.mousePosition) || isFieldDragged)
                {
                    OnHold(e.delta);
                }
            });

            this.eventHandeler = eventHandeler;
        }

        public void ProcessEvents(Event e)
        {
            EventType type = e.type;

            if (eventTypes.ContainsKey(type))
            {
                eventTypes[type].Invoke(e);
            }
        }

		public virtual void Destroy()
        {
            eventHandeler.OnGui -= ProcessEvents;
            eventHandeler.OnElementRemove?.Invoke(this);
        }

        public virtual void OnClickDown()
        {
            eventHandeler.OnElementClick.Invoke(this);
            isBeingDragged = true;
            GUI.changed = true;
            isSelected = true;
        }

        public virtual void OnClickUp()
        {
            eventHandeler.OnElementRelease.Invoke(this);
            isBeingDragged = false;
        }

        public virtual void OnHold(Vector2 delta)
        {
            eventHandeler.OnElementHold.Invoke(this);
        }

        public abstract void Draw();
    }
}

