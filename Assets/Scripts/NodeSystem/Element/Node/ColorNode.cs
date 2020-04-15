using System;
using System.Reflection;
using UnityEngine;

public class ColorNode : Node
{
	[OutputPropperty]
	public Color colorOut1 = Color.white;

	public ColorNode(Vector2 position, float width, Node.Sections sections, Styles style, Actions actions)
	: base(position, width, sections, style, actions)
	{
		this.title = "Color";
		AddConnectionPoints(style);
	}
}
