using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public class ElementDrawer 
    {
        public void Draw(List<Node> elements)
        {
            elements.ForEach(element => element.Draw());
        }
    }
}

