using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public abstract class Element
    {
        protected Vector2 position { set; get; }

        public virtual void Init(Vector2 position)
        {
            this.position = position;
            SystemEventHandeler.OnElementCreate?.Invoke(this);
        }
        public abstract void Draw();
        public virtual void Destroy()
        {
            SystemEventHandeler.OnElementRemove?.Invoke(this);
        }
        public virtual void OnClick()
        {

        }
    }
}

