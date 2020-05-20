using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NodeSystem
{
    public class TextileNode : DropdownNode <TextileData>
    {
        [OutputPropperty]
        public TextileData textileTexture;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            name = "Textiel Node";
            dropdownName = "Textielen";

            foreach (TextileData texture in Resources.LoadAll("Textiles", typeof(TextileData)))
            {
                DropdownElement<TextileData> element = new DropdownElement<TextileData>
                {
                    visual = texture.albedoMap,
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

            GUI.Label(new Rect(0, 120, nodeAreas[2].width, nodeAreas[2].width), textileTexture.albedoMap, styleCenter);
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