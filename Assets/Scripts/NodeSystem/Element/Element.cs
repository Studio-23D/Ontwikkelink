using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public abstract class Element
    {
        public virtual void Init()
        {
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

