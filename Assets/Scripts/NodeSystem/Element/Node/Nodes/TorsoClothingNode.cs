using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem {
	public class TorsoClothingNode : AppearanceItemNode
	{
		[InputPropperty]
		public Texture2D pattern;

        [OutputPropperty]
        public AppearanceItem torso;

		protected override string ResourcePath => "Appearance/Torso";

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			name = "Shirt";

            primaireColor = new Color32(129, 181, 203, 255);
            secondaireColor = Color.white;

            nodeImage = Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Shirt");

            base.Init(position, eventHandeler);
		}

        public override void CalculateChange()
        {
            base.CalculateChange();

            torso = output;

			output.SetPattern(pattern);
		}
	}
}

