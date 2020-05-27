using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem {
	public class FeetClothingNode : AppearanceItemNode
	{
		[InputPropperty]
		public Texture2D patroon;

        //[InputPropperty]
        //public Texture2D materiaal;

		protected override string ResourcePath => "Appearance/Feet";


		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			name = "Schoen Node";
			dropdownName = "Schoenen";

			base.Init(position, eventHandeler);
		}

        public override void CalculateChange()
        {
            base.CalculateChange();
			uitvoer.SetPattern(patroon);
			//uitvoer.SetTextile(materiaal);
		}
	}
}

