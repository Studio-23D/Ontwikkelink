using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractColor : MonoBehaviour
{
	[SerializeField]
	private GameObject colorPallet;
	[SerializeField]
	private Texture2D texture;

	public Color GetColor()
	{
		Color color = Color.white;

		color = texture.GetPixel((Mathf.CeilToInt((((transform.localPosition.x + 100) * .5f) * 10))), (Mathf.CeilToInt((((transform.localPosition.y + 100) * .5f) * 10))));

		return color;
	}
}
