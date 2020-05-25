using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem {
	public class TorsoClothingNode : AppearanceItemNode
	{
		[InputPropperty]
		public Texture2D pattern;

        [InputPropperty]
        public TextileData textile;

		protected override string ResourcePath => "Appearance/Torso";

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			name = "romp Stijlen";
			dropdownName = "Bovenlichaam stijlen kiezen";

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

