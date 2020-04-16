using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public abstract class Element :  MonoBehaviour
    {
        protected Rect rect;
        public abstract void Draw();
    }
}

