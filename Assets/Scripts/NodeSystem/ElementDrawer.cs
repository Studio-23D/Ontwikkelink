using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public class ElementDrawer 
    {
        public void Draw(List<Element> elements, Rect rect)
        {
            GUI.BeginGroup(rect);
            elements.ForEach(element => {
                element.Draw();
            });
            GUI.EndGroup();
        }
    }
}

