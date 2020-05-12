using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem {
    public class HairNode : AppearanceItemNode
	{
        protected override string ResourcePath => "Apparance/Hair";

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{

			name = "Haar Stijlen";
			dropdownName = "Haar stijlen kiezen";

			base.Init(position, eventHandeler);
		}
	}
}

