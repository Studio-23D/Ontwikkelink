using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class PatternNode : DropdownNode<Texture2D>
    {
        [InputPropperty]
        public Color colorR = Color.red;

        [InputPropperty]
        public Color colorG = Color.green;

        [InputPropperty]
        public Color colorB = Color.blue;

        [OutputPropperty]
        public Texture2D patternTexture;

        protected Texture2D pattern;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            name = "Patronen Node";
            dropdownName = "Patronen";

            foreach (Texture2D texture in Resources.LoadAll("Patterns", typeof(Texture2D)))
            {
                DropdownElement<Texture2D> element = new DropdownElement<Texture2D>
                {
                    visual = texture,
                    value = texture,
                };
                dropdownElements.Add(element);
            }

            base.Init(position, eventHandeler);

            CalculateChange();

            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 10));

            rect.size = new Vector2(200, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height);
            originalSize = new Vector2(Size.x, Size.y);
        }

        public override void Draw()
        {
            base.Draw();
            
            GUI.Label(new Rect(0, 120, nodeAreas[2].width, nodeAreas[2].width), patternTexture, styleCenter);
            GUI.Box(nodeAreas[3], "", styleBottomArea);
            GUI.EndGroup();
        }
      
        public override void CalculateChange()
        {
            patternTexture = chosenValue;
            base.CalculateChange();
        }
    }
}