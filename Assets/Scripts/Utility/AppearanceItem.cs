using UnityEngine;

public class AppearanceItem : MonoBehaviour
{
    [SerializeField]
    private Sprite icon;
    public Sprite Icon => icon;

	public void SetColor(Color color)
    {
        this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial.SetColor("BaseColor", color);
    }
    public void SetPattern(Texture2D texture)
    {
        this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial.SetTexture("Pattern", texture);
    }
    public void SetTextile(Texture2D texture)
    {
        this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial.SetTexture("Textile", texture);
    }
}
