using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem {
	public class TorsoClothingNode : ApperanceItemNode
	{
		[InputPropperty]
		public Texture2D pattern;

        [InputPropperty]
        public Texture2D textile;

        protected override string ResourcePath => "Apparance/Torso";

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			name = "Bovenlichaam Stijlen";
			dropdownName = "Bovenlichaam stijlen kiezen";

			base.Init(position, eventHandeler);
		}

        public override void CalculateChange()
        {
            base.CalculateChange();
            apperanceItem.SetPattern(pattern);
            apperanceItem.SetTextile(textile);
        }
    }
}

