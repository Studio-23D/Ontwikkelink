using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeSystem;

public class Idle : InputState
{
    public Idle(SystemEventHandler eventHandler) : base(eventHandler)
    {
        this.conditions.Add(InputTypes.Clicked, new NodeSelected(eventHandler));
        this.conditions.Add(InputTypes.Hold, new Movement(eventHandler));
    }

    public override void OnElementClick(Element element)
    {
        base.OnElementClick(element);
        eventHandler.SelectedElement = element;
        this.ChangeState(InputTypes.Clicked);
    }

    public override void OnElementHover(Element element)
    {
        base.OnElementHover(element);
        this.ChangeState(InputTypes.Hold);
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateLeave()
    {
        base.OnStateLeave();
    }
}
