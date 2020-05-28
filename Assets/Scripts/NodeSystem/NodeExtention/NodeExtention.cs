using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Node))]
public abstract class NodeExtentionBehaviour : MonoBehaviour
{
	protected Node node;
	[SerializeField] protected Transform ExtensionArea;

	public virtual void Init()
	{
		node = GetComponent<Node>();
	}
}
