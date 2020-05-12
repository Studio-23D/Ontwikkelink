using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceItem : MonoBehaviour
{
    [SerializeField]
    private Sprite icon;
    public Sprite Icon => icon;

    public void SetColor(Color color)
    {
		//print(gameObject.GetComponent<MeshRenderer>().sharedMaterial.GetColor("BaseColor"));
        this.gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("BaseColor", color);
        //print(gameObject.GetComponent<MeshRenderer>().sharedMaterial.GetColor("BaseColor"));

    }
}
