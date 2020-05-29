using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeSystem;

public class GarbageCan : MonoBehaviour
{
    [SerializeField] private Image lid;
    [SerializeField] private Image image;
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
        if (element is CharacterNode) return;
        Show();
        if (!rect.Overlaps(element.Rect)) return;
        element.Destroy();
    }
}
