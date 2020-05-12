using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NodeSystem
{
    public class TextileNode : DropdownNode <Texture2D>
    {
        [OutputPropperty]
        public Texture2D textileTexture;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            name = "Textiel Node";
            dropdownName = "Textielen";

            foreach (Texture2D texture in Resources.LoadAll("Textiles", typeof(Texture2D)))
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

            GUI.Label(new Rect(0, 120, nodeAreas[2].width, nodeAreas[2].width), textileTexture, styleCenter);
            GUI.Box(nodeAreas[3], "", styleBottomArea);
            GUI.EndGroup();
        }

        public override void CalculateChange()
        {
            textileTexture = chosenValue;

            base.CalculateChange();
        }
    }
}