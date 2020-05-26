using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Node : Element
{
	private bool isExtendable = false;
	private Dictionary<string, ConnectionPoint> connectionPoints = new Dictionary<string, ConnectionPoint>();

	[Header("Node system")]
	[SerializeField] protected NodeType nodeType;
	[SerializeField] protected ConnectionPoint connectionPointPrefab;
	[SerializeField] protected Transform inputPoints;
	[SerializeField] protected Transform outputPoints;

	protected NodeContent content;
	protected NodeExtentionBehaviour extention;

	public NodeContent Content
	{
		get => content;
		set
		{
			content.Node = this;
			content = value;
		}
	}
	public NodeExtentionBehaviour Extention
	{
		get
		{
			if (isExtendable)
				return extention;
			return null;
		}
	}
	public NodeType Type => nodeType;
	public bool IsExtendable => isExtendable;

	public override void Init()
	{
		base.Init();

		isExtendable = gameObject.TryGetComponent(out extention);

		FieldInfo[] objectFields = content.GetType().GetFields();
		foreach (FieldInfo field in objectFields)
		{
			if (Attribute.IsDefined(field, typeof(InputProppertyAttribute))) {
				AddConnectionPoint(field, inputPoints);
			} else if (Attribute.IsDefined(field, typeof(OutputProppertyAttribute))) {
				AddConnectionPoint(field, outputPoints);
			}		
		}
	}

	public virtual void AddConnectionPoint(FieldInfo field, Transform connections)
	{
		ConnectionPoint point = Instantiate(connectionPointPrefab);
		point.transform.parent = connections;
	}

	public void SetValue<T>(string name, T value)
	{
		//connectionPoints[name];
	}
}
