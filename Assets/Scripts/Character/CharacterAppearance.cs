using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearance : MonoBehaviour
{
    private Character character;

    public Character Character
    {
        get { return character; }
        set { character = value; }
    }

    public void SetAppearanceItem(KeyValuePair<AppearanceItemType, AppearanceItem> appearanceItem)
    {
        Transform itemLocation = character.appearenceItemLocations[appearanceItem.Key];

        if (character.appearenceItemLocations[appearanceItem.Key].childCount != 0)
        {
            foreach(Transform child in itemLocation)
            {
                Destroy(child.gameObject);
            }
        }
        Instantiate(appearanceItem.Value, itemLocation);
    }

    public void SetSkin(Color color)
    {
        character.body.GetComponent<Renderer>().material.SetColor("BaseColor", color);
    }
}
