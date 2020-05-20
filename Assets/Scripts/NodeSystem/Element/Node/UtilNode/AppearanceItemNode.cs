using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
	public abstract class AppearanceItemNode : SliderNode <AppearanceItem>
	{
		protected abstract string ResourcePath {
			get;
		}

        [InputPropperty]
        public Color color = Color.white;

		[OutputPropperty]
		public AppearanceItem appearanceItem;

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
            GetSliderOptions();

            base.Init(position, eventHandeler);

			CalculateChange();
		}

		public override void CalculateChange()
		{
			appearanceItem = chosenValue;

			appearanceItem.SetColor(color);

			base.CalculateChange();
		}

		public override void Draw()
		{
			base.Draw();

			GUI.EndGroup();
		}

        public void GetSliderOptions()
        {
            foreach (AppearanceItem item in Resources.LoadAll<AppearanceItem>(ResourcePath))
            {
                SliderOption<AppearanceItem> option = new SliderOption<AppearanceItem>
                {
                    visual = item.Icon.texture,
                    value = item
                };
                sliderOptions.Add(option);
            }
        }
    }
}

