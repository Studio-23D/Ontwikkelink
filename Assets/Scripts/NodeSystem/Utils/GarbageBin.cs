using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeSystem;

public class GarbageBin : MonoBehaviour
{
    [SerializeField] private Image lid;
    [SerializeField] private Image image;
    [SerializeField] private NodeField nodeField;
	private Rect rect;

    private void Awake()
    {
        image = image ?? GetComponent<Image>();
        rect = this.GetComponent<RectTransform>().rect;
    }

    public void Show()
    {
        image.enabled = true;
        lid.enabled = true;
    }

    public void Hide()
    {
        image.enabled = false;
        lid.enabled = false;
    }

    public void OnElementDrag(Element element)
    {
        if (element is CharacterNode || nodeField.isDragging) return;

        Show();
    }

	public void OnElementRelease(Element element)
	{
		if (rect.Overlaps(element.MainRect) && !(element is CharacterNode))
		{
			element.Destroy();
		}

		Hide();
	}
}
