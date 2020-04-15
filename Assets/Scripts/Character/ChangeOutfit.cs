using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeOutfit : MonoBehaviour
{
	public List<MeshFilter> meshFilters;
	public MeshFilter charMeshFilter;

	public int currentIndex = 0;

	public void Start()
	{
		if (meshFilters[0])
			charMeshFilter.sharedMesh = meshFilters[0].sharedMesh;
	}

	public void ChangeIndexWith(int i)
	{
		if ((currentIndex + i) < 0)
		{
			currentIndex = meshFilters.Count - 1;
		}
		else if ((currentIndex + i) >= meshFilters.Count)
		{
			currentIndex = 0;
		}
		else
		{
			currentIndex += i;
		}

		charMeshFilter.sharedMesh = meshFilters[currentIndex].sharedMesh;
	}
}
