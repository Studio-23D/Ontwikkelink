using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Element : Selectable
{
    public virtual void Init()
    {

    }

    public override void Select()
    {
        base.Select();
    }
}
