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
            
            GUI.Label(new Rect(0, nodeAreas[2].y + 20, nodeAreas[2].width, nodeAreas[2].width), patternTexture, styleCenter);
            GUI.Box(nodeAreas[3], "", styleBottomArea);
            GUI.EndGroup();
        }
      
        public override void CalculateChange()
        {
            pattern = chosenValue;
            patternTexture = new Texture2D(pattern.width, pattern.height, pattern.format, false);
            Graphics.CopyTexture(pattern, patternTexture);

            for (int w = 0; w < pattern.width; w++)
            {
                for (int h = 0; h < pattern.height; h++)
                {
                    if (pattern.GetPixel(w, h).r >= .3)
                    {
                        patternTexture.SetPixel(w, h, colorR);
                    }
                    else if (pattern.GetPixel(w, h).g >= .3)
                    {
                        patternTexture.SetPixel(w, h, colorG);
                    }
                    else if (pattern.GetPixel(w, h).b >= .3)
                    {
                        patternTexture.SetPixel(w, h, colorB);
                    }
                }
            }
            patternTexture.Apply();

            base.CalculateChange();
        }
    }
}