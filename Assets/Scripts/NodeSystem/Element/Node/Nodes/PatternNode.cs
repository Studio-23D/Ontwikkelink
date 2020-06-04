using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class PatternNode : SliderNode<Texture2D>
    {
        [InputPropperty]
        public Color red = Color.red;

        [InputPropperty]
        public Color green = Color.green;

        [InputPropperty]
        public Color blue = Color.blue;

        [OutputPropperty]
        public Texture2D pattern;

        protected Texture2D texture;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            name = "Patroon";
            primaireColor = new Color32(225, 98, 100, 255);
            secondaireColor = Color.white;

            nodeImage = Resources.Load<Texture2D>("NodeSystem/Overhaul/Icon_Patroon");

            foreach (Texture2D texture in Resources.LoadAll("Patterns", typeof(Texture2D)))
            {
                SliderElement<Texture2D> element = new SliderElement<Texture2D>
                {
                    visual = texture,
                    value = texture,
                };
                sliderElements.Add(element);
            }

            base.Init(position, eventHandeler);

            CalculateChange();
        }

        public override void Draw()
        {
            base.Draw();

            GUI.EndGroup();
        }
      
        public override void CalculateChange()
        {
			texture = chosenValue;
			pattern = new Texture2D(texture.width, texture.height, texture.format, false);
            Graphics.CopyTexture(texture, pattern);

            for (int w = 0; w < texture.width; w++)
            {
                for (int h = 0; h < texture.height; h++)
                {
                    if (texture.GetPixel(w, h).r >= .3)
                    {
						pattern.SetPixel(w, h, red);
                    }
                    else if (texture.GetPixel(w, h).g >= .3)
                    {
						pattern.SetPixel(w, h, green);
                    }
                    else if (texture.GetPixel(w, h).b >= .3)
                    {
						pattern.SetPixel(w, h, blue);
                    }
                }
            }
			pattern.Apply();
            currentVisual = pattern;

            base.CalculateChange();
        }
    }
}