using System;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
	[SerializeField] private Transform characterContainer;

	public void SetCharacter(Transform transform)
	{
		foreach (Transform character in characterContainer)
		{
			Destroy(character.gameObject);
		}

		Instantiate(transform, characterContainer);
	}
}
