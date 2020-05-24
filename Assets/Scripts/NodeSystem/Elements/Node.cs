using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Node : Element
{
	[Header("Point system")]
	[SerializeField] protected ConnectionPoint connectionPointPrefab;
	[SerializeField] protected Transform inputPoints;
	[SerializeField] protected Transform outputPoints;


	private NodeContent nodeContent;

	public NodeContent NodeContent
	{
		get => nodeContent;
		set => nodeContent = value;
	}

	public override void Init()
	{
		base.Init();

		nodeContent = new NodeContent();

		FieldInfo[] objectFields = nodeContent.GetType().GetFields();
		Debug.Log(objectFields);
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
}
