using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearence : MonoBehaviour
{
	#region PUBLIC_MEMBERS 

	public Transform HairItem
	{
		get
		{
			return character.GetBody.hair.GetChild(0);
		}
		set
		{
			if (character.GetBody.hair.childCount > 0)
				Destroy(character.GetBody.hair.GetChild(0).gameObject);

			if (value)
				Instantiate(value, character.GetBody.hair);
		}
	}

	public Transform TorsoItem
	{
		get
		{
			return character.GetBody.torso.GetChild(0);
		}
		set
		{
			if (character.GetBody.torso.childCount >= maxTorsoItems)
				Destroy(character.GetBody.torso.GetChild(0).gameObject);

			if (value)
				Instantiate(value, character.GetBody.torso);
		}
	}

	public Transform LegsItem
	{
		get
		{
			return character.GetBody.legs.GetChild(0);
		}
		set
		{
			if (character.GetBody.legs.childCount >= maxLegsItems)
				Destroy(character.GetBody.legs.GetChild(0).gameObject);

			if (value)
				Instantiate(value, character.GetBody.legs);
		}
	}

	public Transform FeetItem
	{
		get
		{
			return character.GetBody.feet.GetChild(0);
		}
		set
		{
			if (character.GetBody.feet.childCount >= maxFeetItems)
				Destroy(character.GetBody.feet.GetChild(0).gameObject);

			if (value)
				Instantiate(value, character.GetBody.feet);
		}
	}

	#endregion



	#region PRIVATE_MEMBERS

	[SerializeField]
	[Tooltip("Sets appearence objects to index 0 objects at start")]
	private bool setDefault = false;
	[SerializeField]
	[Tooltip("Max amount of items the torso can hold")]
	private int maxTorsoItems = 1;
	[SerializeField]
	[Tooltip("Max amount of items the legs can hold")]
	private int maxLegsItems = 1;
	[SerializeField]
	[Tooltip("Max amount of items the feet can hold")]
	private int maxFeetItems = 1;


	private string characterTag = "Character";

	private Character character;

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

	private void Update()
	{
		if (!character)
		{
			character = GameObject.FindGameObjectWithTag(characterTag).GetComponent<Character>();
		}
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
