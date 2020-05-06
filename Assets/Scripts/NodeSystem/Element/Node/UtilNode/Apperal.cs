using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
	public class Apperal <T> : DropdownNode
	{


		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			base.Init(position, eventHandeler);

			name = "OverideMe";
			dropdownName = "Uiterlijk Artikelen";

		}

		public override void CalculateChange()
		{
			base.CalculateChange();
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}

