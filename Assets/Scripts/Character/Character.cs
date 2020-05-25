using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform body;

    private Dictionary<AppearanceItemType, AppearanceItem> appearanceItems = new Dictionary<AppearanceItemType, AppearanceItem>();
    private Animator animator;
    [SerializeField] private new Renderer renderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = renderer ?? GetComponent<Renderer>();
    }

    public void AddItem(AppearanceItem item)
    {
        if (item.HasInCompatibilities)
        {
            foreach (AppearanceItemType type in item.InCompatibleWith)
            {
                if (!appearanceItems.ContainsKey(type))
                {
                    RemoveItem(type);
                }
            }
        }

        if (appearanceItems.ContainsKey(item.AppearanceItemtype))
            RemoveItem(item.AppearanceItemtype);
        AppearanceItem itemAsGameObject = Instantiate<AppearanceItem>(item, body);
        appearanceItems.Add(item.AppearanceItemtype, itemAsGameObject);
        animator.Play("Idle", -1, 0);

        //itemAsGameObject.Animator.Play("Idle", -1, 0);
    }

    public void RemoveItem(AppearanceItemType type)
    {
        Destroy(appearanceItems[type].gameObject);
        appearanceItems.Remove(type);
    }

    public void SetSkinCollor(Color color)
    {
        renderer.material.SetColor("BaseColor", color);
    }
}
