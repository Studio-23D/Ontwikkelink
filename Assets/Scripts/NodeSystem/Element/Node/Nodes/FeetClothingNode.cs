using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem {
	public class FeetClothingNode : AppearanceItemNode
	{
		[InputPropperty]
		public Texture2D pattern;

        [InputPropperty]
        public Texture2D textile;

		protected override string ResourcePath => "Appearance/Feet";


		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			name = "Voeten Stijlen";
			dropdownName = "Voeten stijlen kiezen";

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

