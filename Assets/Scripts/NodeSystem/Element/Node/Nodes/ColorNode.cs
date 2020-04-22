using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
	public class ColorNode : Node
	{

		[InputPropperty]
		public Color colorOut1 = Color.white;

		[OutputPropperty]
		public Color colorOut2 = Color.white;

		public override void CalculateChange()
		{
			throw new NotImplementedException();
		}

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			base.Init(position, eventHandeler);
            name = "Color Node";
		}

        public override void Draw()
        {
            base.Draw();
            
            GUI.EndGroup();
        }
    }
}

