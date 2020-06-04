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

		public AppearanceItem output;

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			foreach (AppearanceItem item in Resources.LoadAll<AppearanceItem>(ResourcePath))
            {
				SliderElement<AppearanceItem> element = new SliderElement<AppearanceItem>
				{
					visual = item.Icon.texture,
					value = item
				};

				sliderElements.Add(element);
			}

            connectionPointStartOffset = 90;
            connectionPointOffset = 40;

            height = 310;
            width = 180;

            base.Init(position, eventHandeler);

			CalculateChange();
		}

		public override void CalculateChange()
		{
            currentVisual = sliderElements[current].visual;

            this.output = chosenValue;

			this.output.SetColor(color);

			base.CalculateChange();
		}

		public override void Draw()
		{
			base.Draw();

			GUI.EndGroup();
		}
	}
}

