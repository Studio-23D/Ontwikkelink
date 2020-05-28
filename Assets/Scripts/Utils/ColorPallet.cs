using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPallet : Selectable
{
    private Color value;
    private Vector2 startPosition;

    [SerializeField] private RectTransform handle;

    public Action<Color> OnValueChange = delegate { };

    public RectTransform RectTransform { get; private set; }
   
    public bool IsInBounds
    {
        get
        {
            print(handle.localPosition + " " + RectTransform.rect);
            Vector2 position = handle.localPosition;
            Rect rect = RectTransform.rect;
            return position.x < rect.width &&
                position.x > 0 &&
                position.y < rect.height&&
                position.y > 0;
        }
    }

    public bool IsDragging { get; private set; } = false;
    public Color Value => value;

    protected override void Awake()
    {
        base.Awake();
        RectTransform = GetComponent<RectTransform>();
        handle = handle != null ? handle : transform.GetChild(0).GetComponent<RectTransform>();
        startPosition = handle.localPosition;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        IsDragging = true;
        startPosition = handle.localPosition;
        OnValueChange?.Invoke(value);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        IsDragging = false;
    }

    private void Update()
    {
        if(IsDragging && IsInBounds)
            handle.position = Input.mousePosition;
        if (!IsInBounds && IsDragging)
            handle.localPosition = startPosition;
    }
}
