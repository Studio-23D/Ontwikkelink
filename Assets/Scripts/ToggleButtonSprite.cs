using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonSprite : MonoBehaviour
{
    [SerializeField] private Sprite spriteA;
    [SerializeField] private Sprite spriteB;

    [SerializeField] private Image targetGraphic;

    private bool toggle = true;

    public void ToggleSprite()
    {
        toggle = !toggle;

        if(toggle)
        {
            targetGraphic.sprite = spriteA;
        }
        else
        {
            targetGraphic.sprite = spriteB;
        }
    }
}
