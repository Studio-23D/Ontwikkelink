using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderSelectionManager : MonoBehaviour
{
	[SerializeField] private Transform characterContainer;

    public void SetGender(Transform transform)
    {
		if (characterContainer.childCount == 0)
			Instantiate(transform, characterContainer);
    }
}
