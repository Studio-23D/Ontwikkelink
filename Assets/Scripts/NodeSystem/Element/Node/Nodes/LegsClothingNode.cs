using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem {
	public class LegsClothingNode : AppearanceItemNode
	{
		[InputPropperty]
		public Texture2D pattern;

        [OutputPropperty]
        public AppearanceItem legs;

        protected override string ResourcePath => "Appearance/Legs";


		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			name = "Broek";

            primaireColor = new Color32(127, 134, 179, 255);
            secondaireColor = Color.white;

            nodeImage = Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Broek");

            base.Init(position, eventHandeler);
		}

        public override void CalculateChange()
        {
            base.CalculateChange();

            legs = output;

			output.SetPattern(pattern);
		}
	}
}

