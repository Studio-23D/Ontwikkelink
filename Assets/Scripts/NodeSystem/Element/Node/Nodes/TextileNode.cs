using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NodeSystem
{
    public class TextileNode : DropdownNode <TextileData>
    {
        [OutputPropperty]
        public TextileData materiaal;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            name = "Materiaal Node";
            dropdownName = "Materialen";

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

            mainRect.size = new Vector2(200, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height);
            originalSize = new Vector2(MainSize.x, MainSize.y);
        }

        public override void Draw()
        {
            base.Draw();

            GUI.Label(new Rect(0, nodeAreas[2].y + 20, nodeAreas[2].width, nodeAreas[2].width), materiaal.albedoMap);
            GUI.Box(nodeAreas[3], "", styleBottomArea);
            GUI.EndGroup();
        }

        public override void CalculateChange()
        {
			materiaal = chosenValue;

            base.CalculateChange();
        }
    }
}