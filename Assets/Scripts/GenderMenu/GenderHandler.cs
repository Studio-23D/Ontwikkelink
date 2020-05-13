using System;
using UnityEngine;

public class GenderHandler : MonoBehaviour
{
	[SerializeField] private Transform characterContainer;

	public void SetGender(Transform transform)
	{
		Instantiate(transform, characterContainer);
	}

	public GameObject SetGender(GameObject character)
    {
	    return Instantiate(character, characterContainer);
    }
}
