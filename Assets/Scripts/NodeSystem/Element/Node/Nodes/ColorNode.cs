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

		public override void Init()
		{
			

		}
	}
}

