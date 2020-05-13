using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem {
	public class LegsClothingNode : AppearanceItemNode
	{
		[InputPropperty]
		public Texture2D pattern;

        [InputPropperty]
        public Texture2D textile;

		protected override string ResourcePath => "Appearance/Legs";


		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			name = "Benen Stijlen";
			dropdownName = "Benen stijlen kiezen";

			base.Init(position, eventHandeler);
		}

        public override void CalculateChange()
        {
            base.CalculateChange();
            appearanceItem.SetPattern(pattern);
            appearanceItem.SetTextile(textile);
        }
    }
}

