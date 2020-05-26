using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceItem : MonoBehaviour
{
    [SerializeField] private AppearanceItemType appearanceItemType;
    public AppearanceItemType AppearanceItemtype => appearanceItemType;
    [SerializeField] private List<AppearanceItemType> inCompatibleWith;
    public List<AppearanceItemType> InCompatibleWith;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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

    public Animator Animator => animator;
}
