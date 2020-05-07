using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearence : MonoBehaviour
{
    private Character character;

    public Character Character
    {
        get { return character; }
        set { character = value; }
    }

    public void SetAppearenceItem(KeyValuePair<string, GameObject> appearenceItem)
    {
        if (character.appearenceItemLocations[appearenceItem.Key].childCount != 0)
        {
            Destroy(character.appearenceItemLocations[appearenceItem.Key].GetChild(0));
        }
        Instantiate(appearenceItem.Value, character.appearenceItemLocations[appearenceItem.Key]);
    }
}
