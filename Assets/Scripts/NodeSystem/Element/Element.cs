using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public abstract class Element
    {
		public Vector2 SetStartPosition(Vector2 startPosition) => this.startPosition = startPosition;
		public Vector2 GetStartPosition => startPosition;
		protected Vector2 startPosition;

		protected Action<Element> onClick = delegate { };
        protected Action<Element> onHover = delegate { };

        protected Rect rect = new Rect();
        public virtual Rect Rect => rect;
        public virtual Vector2 Position => rect.position;
        public Vector2 Size => rect.size;
        protected SystemEventHandeler eventHandeler;

		public virtual void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
			startPosition = position;

			Vector2 size = new Vector2(100, 100);
            rect = new Rect(position, size);
            SystemEventHandeler.OnElementCreate?.Invoke(this);
            this.eventHandeler = eventHandeler;
            this.eventHandeler.SubscribeTo(EventType.MouseDown, () => CheckClick());
            this.eventHandeler.CheckHover += () => CheckHover();
            onClick += (Element element) => SystemEventHandeler.OnElementClick.Invoke(element);
            onHover += (Element element) => SystemEventHandeler.OnElementHover.Invoke(element);
		}

		public void ResetPosition()
		{
			rect.position = startPosition;
		}

		public virtual void Destroy()
        {
            SystemEventHandeler.OnElementRemove?.Invoke(this);
        }

        private void CheckClick()
        {
            if (!Rect.Contains(SystemEventHandeler.mousePosition)) return;
            onClick?.Invoke(this);
        }

        private void CheckHover()
        {
            if (!Rect.Contains(SystemEventHandeler.mousePosition)) return;
            onHover?.Invoke(this);
        }

        public virtual void OnClick(Action<Element> action)
        {
            this.onClick += action;
        }

        public virtual void OnHover(Action<Element> action)
        {
            this.onHover += action;
        }

        public abstract void Draw();
    }
}

