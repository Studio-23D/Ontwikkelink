using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class PatternNode : DropdownNode
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

        //Material material = new Material(Shader.Find("NodeShaders/PatternShader"));

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            name = "Pattern Node";
            dropdownName = "Patterns";

            foreach (Texture2D texture in Resources.LoadAll("Patterns", typeof(Texture2D)))
            {
                DropdownElement element = new DropdownElement
                {
                    visual = texture,
                    value = texture
                };
                dropdownElements.Add(element);
            }

            base.Init(position, eventHandeler);

            pattern = chosenValue as Texture2D;

            patternTexture = new Texture2D(pattern.width, pattern.height, pattern.format, false);

            Graphics.CopyTexture(pattern, patternTexture);

            nodeAreas.Add(new Rect(0, CalculateAreaY(3), 200, 10));

            rect.size = new Vector2(200, CalculateAreaY(nodeAreas.Count));
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
            pattern = chosenValue as Texture2D;
            patternTexture = new Texture2D(pattern.width, pattern.height, pattern.format, false);
            Graphics.CopyTexture(pattern, patternTexture);
            
            for (int w = 0; w < pattern.width; w++)
            {
                for (int h = 0; h < pattern.height; h++)
                {
                    //Debug.Log(pattern.GetPixel(w, h));
                    if (pattern.GetPixel(w, h).r >= .5)
                    {
                        patternTexture.SetPixel(w, h, colorR);
                        //Debug.Log("setPixel R");
                    }
                    else if (pattern.GetPixel(w, h).g >= .5)
                    {
                        patternTexture.SetPixel(w, h, colorG);
                        //Debug.Log("setPixel G");
                    }
                    else if (pattern.GetPixel(w, h).b >= .5)
                    {
                        patternTexture.SetPixel(w, h, colorB);
                        //Debug.Log("setPixel B");
                    }
                }
            }
            patternTexture.Apply();
        }
    }
}