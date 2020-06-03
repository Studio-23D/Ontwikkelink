using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class PatternNode : DropdownNode<Texture2D>
    {
        [InputPropperty]
        public Color rood = Color.red;

        [InputPropperty]
        public Color groen = Color.green;

        [InputPropperty]
        public Color blauw = Color.blue;

        [OutputPropperty]
        public Texture2D patroon;

        protected Texture2D texture;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            name = "Patroon Node";
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
            
            GUI.Label(new Rect(0, nodeAreas[2].y + 20, nodeAreas[2].width, nodeAreas[2].width), patroon);
            GUI.Box(nodeAreas[3], "", styleBottomArea);
            GUI.EndGroup();
        }
      
        public override void CalculateChange()
        {
			texture = chosenValue;
			patroon = new Texture2D(texture.width, texture.height, texture.format, false);
            Graphics.CopyTexture(texture, patroon);

            for (int w = 0; w < texture.width; w++)
            {
                for (int h = 0; h < texture.height; h++)
                {
                    if (texture.GetPixel(w, h).r >= .3)
                    {
						patroon.SetPixel(w, h, rood);
                    }
                    else if (texture.GetPixel(w, h).g >= .3)
                    {
						patroon.SetPixel(w, h, groen);
                    }
                    else if (texture.GetPixel(w, h).b >= .3)
                    {
						patroon.SetPixel(w, h, blauw);
                    }
                }
            }
			patroon.Apply();

            base.CalculateChange();
        }
    }
}