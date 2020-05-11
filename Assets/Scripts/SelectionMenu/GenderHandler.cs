using System;
using UnityEngine;

public class GenderHandler : MonoBehaviour
{
	[SerializeField]
    private Transform characterContainer;

    public GameObject SetGender(GameObject character)
    {
	    return Instantiate(character, characterContainer);
    }
}
