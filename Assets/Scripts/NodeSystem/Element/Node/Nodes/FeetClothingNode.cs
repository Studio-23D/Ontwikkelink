using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem {
	public class FeetClothingNode : AppearanceItemNode
	{
		[InputPropperty]
		public Texture2D pattern;

        [OutputPropperty]
        public AppearanceItem shoes;

        protected override string ResourcePath => "Appearance/Feet";


		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			name = "Schoen";

            primaireColor = new Color32(236, 190, 103, 255);
            secondaireColor = Color.white;

            nodeImage = Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Shoenen");

            base.Init(position, eventHandeler);
		}

        public override void CalculateChange()
        {
            base.CalculateChange();

            shoes = output;

            output.SetPattern(pattern);
		}
	}
}

