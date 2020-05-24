using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Element : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightedColor = Color.grey;
    [SerializeField] private Color selectedColor = new Color(200, 200, 200);

    protected Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        this.Init();
    }

    public virtual void Init()
    {
        Color color = image.color;
        color = color.CombineColors(highlightedColor);
        image.color = color;
    }
}
