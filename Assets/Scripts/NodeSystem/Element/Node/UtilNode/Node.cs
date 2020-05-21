using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public enum ConnectionPointType { In, Out }

namespace NodeSystem
{
	public abstract class Node : Element
	{
        [SerializeField] private List<ConnectionPoint> inputPoints;
        [SerializeField] private List<ConnectionPoint> outputPoints;

        public Action OnChange = delegate { };
        
        public virtual void CalculateChange()
        {
            OnChange?.Invoke();
        }
    }
}