using System;
using UnityEngine;

namespace NodeSystem
{
    public class Element : MonoBehaviour
    {
        protected Action<Element> onClick = delegate { };
        protected Action<Element> onHover = delegate { };

        protected Rect rect;

        protected SystemEventHandeler eventHandeler;

        public virtual void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            rect = GetComponent<RectTransform>().rect;

            SystemEventHandeler.OnElementCreate?.Invoke(this);

            this.eventHandeler = eventHandeler;
            this.eventHandeler.SubscribeTo(EventType.MouseDown, () => CheckClick());
            this.eventHandeler.CheckHover += () => CheckHover();

            onClick += (Element element) => SystemEventHandeler.OnElementClick.Invoke(element);
            onHover += (Element element) => SystemEventHandeler.OnElementHover.Invoke(element);
        }

        public virtual void Destroy()
        {
            SystemEventHandeler.OnElementRemove?.Invoke(this);
        }

        private void CheckClick()
        {
            if (!rect.Contains(SystemEventHandeler.mousePosition)) return;
            onClick?.Invoke(this);
        }

        private void CheckHover()
        {
            if (!rect.Contains(SystemEventHandeler.mousePosition)) return;
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
    }
}