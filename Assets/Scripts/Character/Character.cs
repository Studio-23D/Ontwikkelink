using System;
using UnityEngine;

public class Character : MonoBehaviour
{
	#region PUBLIC_STRUCTS

	[Serializable]
	public struct Body
	{
		[Header("References")]
		[Tooltip("Holds the hair object")]
		public Transform hair;
		public Transform torso;
		public Transform legs;
		public Transform feet;
	}

	#endregion

	public Body GetBody { get { return body; } }

	[SerializeField]
	[Tooltip("Holds all container references")]
	private Body body;
}
