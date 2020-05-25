using System.Collections.Generic;
using UnityEngine;

public class AppearanceItem : MonoBehaviour
{
    [SerializeField]
    private Sprite icon;

    [SerializeField] private AppearanceItemType appearanceItemType;
    public AppearanceItemType AppearanceItemtype => appearanceItemType;
    [SerializeField] private List<AppearanceItemType> inCompatibleWith;
    public List<AppearanceItemType> InCompatibleWith => inCompatibleWith;
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

    public void SetColor(Color color)
    {
        this.gameObject.GetComponent<Renderer>().sharedMaterial.SetColor("BaseColor", color);
    }
    public void SetPattern(Texture2D texture)
    {
        this.gameObject.GetComponent<Renderer>().sharedMaterial.SetTexture("Pattern", texture);
    }
    public void SetTextile(TextileData texture)
    {
        //this.gameObject.GetComponent<Renderer>().sharedMaterial.SetTexture("Textile", texture.albedoMap);
    }
}
