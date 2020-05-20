using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NodeSystem
{
    public class TextileNode : SliderNode <TextileData>
    {
        [OutputPropperty]
        public TextileData textileTexture;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            name = "Textiel Node";
            dropdownName = "Textielen";

            GetSliderOptions();

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
            textileTexture = chosenValue;

            base.CalculateChange();
        }

        public void GetSliderOptions()
        {
            foreach (TextileData texture in Resources.LoadAll("Textiles", typeof(TextileData)))
            {
                SliderOption<TextileData> option = new SliderOption<TextileData>
                {
                    visual = texture.albedoMap,
                    value = texture,
                };
                sliderOptions.Add(option);
            }
        }
    }
}