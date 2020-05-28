using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeContent
{
	protected Node node;
	public Node Node
	{
		get => node;
		set => node = value;
	}
}
