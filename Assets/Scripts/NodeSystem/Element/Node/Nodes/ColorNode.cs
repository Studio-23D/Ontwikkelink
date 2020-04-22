using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
	public class ColorNode : Node
	{
		[OutputPropperty]
		public Color colorOut1 = Color.white;

        public override void CalculateChange()
		{
			throw new NotImplementedException();
		}

		public override void Init(Vector2 position)
		{
			base.Init(position);
            name = "Color Node";
		}

        public override void Draw()
        {
            base.Draw();
            
            MyGUI.DrawLine(new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y), new Vector2(0, 0), Color.black, 3);

            GUI.EndGroup();
        }
    }
}

