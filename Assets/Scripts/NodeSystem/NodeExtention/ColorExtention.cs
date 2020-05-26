using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorExtention : NodeExtentionBehaviour
{
    [SerializeField] private ColorPallet colorPallet;


    private void Start()
    {
        colorPallet.OnValueChange += (Color color) => this.node.SetValue("color", color);
    }

}
