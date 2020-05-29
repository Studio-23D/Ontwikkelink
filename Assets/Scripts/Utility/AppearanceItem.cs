using System.Collections.Generic;
using UnityEngine;

public class AppearanceItem : MonoBehaviour
{
	public List<AppearanceItemType> InCompatibleWith => inCompatibleWith;
	public AppearanceItemType AppearanceItemtype => appearanceItemType;
	public Sprite Icon => icon;
	public Texture SkinTexture => skinTexture;

	public bool HasInCompatibilities
	{
		get
		{
			if (inCompatibleWith.Count > 0)
			{
				return true;
			}
			return false;
		}
	}

	[SerializeField] private Sprite icon;
	[SerializeField] private Renderer renderer;
	[SerializeField] private AnimationClip animation;
	[SerializeField] private Texture skinTexture;
	[SerializeField] private AppearanceItemType appearanceItemType;
	[SerializeField] private List<AppearanceItemType> inCompatibleWith;
	private Material material;

	public void SetColor(Color color)
	{
		Material material = Instantiate(renderer.sharedMaterial);
		material.SetColor("BaseColor", color);
		renderer.material = material;
    }

    public void SetPattern(Texture2D texture)
    {
		Material material = Instantiate(renderer.sharedMaterial);
		renderer.sharedMaterial.SetTexture("Pattern", texture);
		renderer.material = material;
	}

    public void SetTextile(TextileData texture)
    {
        //this.gameObject.GetComponent<Renderer>().sharedMaterial.SetTexture("Textile", texture.albedoMap);
    }
}
