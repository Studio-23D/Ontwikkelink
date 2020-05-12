using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
	public abstract class AppearanceItemNode : DropdownNode <ApperanceItem>
	{
		protected abstract string ResourcePath {
			get;
		}

		[OutputPropperty]
		public ApperanceItem apperanceItem;

		[InputPropperty]
		public Color color = Color.white;

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{

			foreach (ApperanceItem item in Resources.LoadAll<ApperanceItem>(ResourcePath)){
				DropdownElement<ApperanceItem> element = new DropdownElement<ApperanceItem>
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
			apperanceItem = chosenValue;

			apperanceItem.SetColor(color);

			base.CalculateChange();
		}

		public override void Draw()
		{
			base.Draw();

			GUI.Label(new Rect(0, 120, nodeAreas[2].width, nodeAreas[2].width), apperanceItem.Icon.texture, styleCenter);
			GUI.Box(nodeAreas[3], "", styleBottomArea);
			GUI.EndGroup();
		}
	}
}

