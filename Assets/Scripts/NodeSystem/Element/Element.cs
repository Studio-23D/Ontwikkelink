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
        protected Action<Element> onDubbleClick = delegate { };
        protected Action<Element, Vector3> onHold = delegate { };
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
            this.eventHandeler.OnInput += CheckInput;
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

        private void CheckInput(InputTypes input)
        {
            if (!Rect.Contains(eventHandeler.MousePosition)) return;
            switch (input)
            {
                case InputTypes.Clicked:
                    onClick?.Invoke(this);
                    break;
                case InputTypes.DubbleClick:
                    onDubbleClick?.Invoke(this);
                    break;
                case InputTypes.Hover:
                    onHover?.Invoke(this);
                    break;
                case InputTypes.Hold:
                    onHold?.Invoke(this, eventHandeler.MousePosition);
                    break;
                default:

                    break;
            }
        }

        public virtual void OnClick()
        {
            
        }

        public virtual void OnHover()
        {
            
        }

        public abstract void Draw();
    }
}

