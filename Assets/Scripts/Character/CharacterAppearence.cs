using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearence : MonoBehaviour
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



	#region PUBLIC_MEMBERS 

	public Transform HairItem
	{
		get
		{
			return body.hair.GetChild(0);
		}
		set
		{
			if (body.hair.childCount > 0)
				Destroy(body.hair.GetChild(0).gameObject);

			if (value)
				Instantiate(value, body.hair);
		}
	}

	public Transform TorsoItem
	{
		get
		{
			return body.torso.GetChild(0);
		}
		set
		{
			if (body.torso.childCount >= maxTorsoItems)
				Destroy(body.torso.GetChild(0).gameObject);

			if (value)
				Instantiate(value, body.torso);
		}
	}

	public Transform LegsItem
	{
		get
		{
			return body.legs.GetChild(0);
		}
		set
		{
			if (body.legs.childCount >= maxLegsItems)
				Destroy(body.legs.GetChild(0).gameObject);

			if (value)
				Instantiate(value, body.legs);
		}
	}

	public Transform FeetItem
	{
		get
		{
			return body.feet.GetChild(0);
		}
		set
		{
			if (body.feet.childCount >= maxFeetItems)
				Destroy(body.feet.GetChild(0).gameObject);

			if (value)
				Instantiate(value, body.feet);
		}
	}

	#endregion



	#region PRIVATE_MEMBERS

	[SerializeField]
	[Tooltip("Sets appearence objects to index 0 objects at start")]
	private bool setDefault = false;
	[Tooltip("Max amount of items the torso can hold")]
	public int maxTorsoItems = 1;
	[Tooltip("Max amount of items the legs can hold")]
	public int maxLegsItems = 1;
	[Tooltip("Max amount of items the feet can hold")]
	public int maxFeetItems = 1;
	[SerializeField]
	[Tooltip("Holds all container references")]
	private Body body;

	#endregion



	#region MONOBEHAVIOUR_METHODS

	private void Start()
	{
		if (!setDefault) return;

		if (testHairStyles[1])
			HairItem = testHairStyles[1];

		if (testTorsoItems[1])
			TorsoItem = testTorsoItems[1];

		if (testLegsItems[1])
			LegsItem = testLegsItems[1];

		if (testFeetItems[1])
			FeetItem = testFeetItems[1];
	}

	#endregion



	#region DEMO

	public List<Transform> testHairStyles;
	private int hairIndex = 0;
	public List<Transform> testTorsoItems;
	private int torsoIndex = 0;
	public List<Transform> testLegsItems;
	private int legsIndex = 0;
	public List<Transform> testFeetItems;
	private int feetIndex = 0;

	public void ChangeHair(int i)
	{
		if (hairIndex + i >= testHairStyles.Count)
		{
			hairIndex = 0;
		}
		else if (hairIndex + i < 0)
		{
			hairIndex = testHairStyles.Count - 1;
		}
		else
		{
			hairIndex += i;
		}

		HairItem = testHairStyles[hairIndex];
	}

	public void ChangeTorso(int i)
	{
		if (torsoIndex + i >= testTorsoItems.Count)
		{
			torsoIndex = 0;
		}
		else if (torsoIndex + i < 0)
		{
			torsoIndex = testTorsoItems.Count - 1;
		}
		else
		{
			torsoIndex += i;
		}

		TorsoItem = testTorsoItems[torsoIndex];
	}

	public void ChangeLegs(int i)
	{
		if (legsIndex + i >= testLegsItems.Count)
		{
			legsIndex = 0;
		}
		else if (legsIndex + i < 0)
		{
			legsIndex = testLegsItems.Count - 1;
		}
		else
		{
			legsIndex += i;
		}

		LegsItem = testLegsItems[legsIndex];
	}

	public void ChangeFeet(int i)
	{
		if (feetIndex + i >= testFeetItems.Count)
		{
			feetIndex = 0;
		}
		else if (feetIndex + i < 0)
		{
			feetIndex = testFeetItems.Count - 1;
		}
		else
		{
			feetIndex += i;
		}

		FeetItem = testFeetItems[feetIndex];
	}

	#endregion
}
