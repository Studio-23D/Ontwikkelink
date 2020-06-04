using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public class HairNode : AppearanceItemNode
	{
        protected override string ResourcePath => "Appearance/Hair";

        [OutputPropperty]
        public AppearanceItem hair;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			name = "Haar";

            primaireColor = new Color32(237, 180, 211, 255);
            secondaireColor = Color.white;

            nodeImage = Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Haar");

            base.Init(position, eventHandeler);
		}

        public override void CalculateChange()
        {
            base.CalculateChange();

            hair = output;
        }
    }
}

