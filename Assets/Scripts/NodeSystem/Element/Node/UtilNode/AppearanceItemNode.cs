using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
	public abstract class AppearanceItemNode : DropdownNode <AppearanceItem>
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
			foreach (AppearanceItem item in Resources.LoadAll<AppearanceItem>(ResourcePath)){
				DropdownElement<AppearanceItem> element = new DropdownElement<AppearanceItem>
				{
					visual = item.Icon.texture,
					value = item
				};
				dropdownElements.Add(element);
			}

			base.Init(position, eventHandeler);

			CalculateChange();

			nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 10));

			rect.size = new Vector2(200, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height);
			originalSize = new Vector2(Size.x, Size.y);
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

			GUI.Label(new Rect(0, nodeAreas[2].y + 20, nodeAreas[2].width, nodeAreas[2].width), appearanceItem.Icon.texture, styleCenter);
			GUI.Box(nodeAreas[3], "", styleBottomArea);
			GUI.EndGroup();
		}
	}
}

