using System.Collections.Generic;
using UnityEngine;

public class AppearanceItem : MonoBehaviour
{
	public List<AppearanceItemType> InCompatibleWith => inCompatibleWith;
	public AppearanceItemType AppearanceItemtype => appearanceItemType;
	public Sprite Icon => icon;
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
	[SerializeField] private AppearanceItemType appearanceItemType;
	[SerializeField] private List<AppearanceItemType> inCompatibleWith;


	public void SetColor(Color color)
	{
        renderer.sharedMaterial.SetColor("BaseColor", color);
    }

    public void SetPattern(Texture2D texture)
    {
		renderer.sharedMaterial.SetTexture("Pattern", texture);
    }

    public void SetTextile(TextileData texture)
    {
        //this.gameObject.GetComponent<Renderer>().sharedMaterial.SetTexture("Textile", texture.albedoMap);
    }
}
