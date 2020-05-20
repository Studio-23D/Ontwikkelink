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

        public int drawOrder = 0;

        protected Rect rect = new Rect();
        public virtual Rect Rect => rect;
        public virtual Vector2 Position => rect.position;
        public Vector2 Size => rect.size;
        protected SystemEventHandler eventHandeler;

		public virtual void Init(Vector2 position, SystemEventHandler eventHandeler)
        {
			startPosition = position;

			Vector2 size = new Vector2(100, 100);
            rect = new Rect(position, size);
            this.eventHandeler = eventHandeler;
            eventHandeler.OnElementCreate?.Invoke(this);
            this.eventHandeler.OnClick += CheckClick;
            this.eventHandeler.OnHover += () => CheckHover();
            onClick += (Element element) => eventHandeler.OnElementClicked.Invoke(element);
            onHover += (Element element) => eventHandeler.OnElementHover.Invoke(element);
		}

		public void ResetPosition()
		{
			rect.position = startPosition;
		}

		public virtual void Destroy()
        {
           eventHandeler.OnElementDestroy?.Invoke(this);
        }

        private void CheckClick()
        {
            if (!Rect.Contains(eventHandeler.MousePosition)) return;
            onClick?.Invoke(this);
        }

        private void CheckHover()
        {
            if (!Rect.Contains(eventHandeler.MousePosition)) return;
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

