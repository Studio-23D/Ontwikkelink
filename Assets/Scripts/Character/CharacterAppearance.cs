using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearance : MonoBehaviour
{
	public Character Character
	{
		get { return character; }
		set { character = value; }
	}

	private Character character;

    public void SetAppearanceItem(KeyValuePair<AppearanceItemType, AppearanceItem> appearanceItem)
    {
        character.AddItem(appearanceItem.Value);
    }

    public void SetSkin(Color color)
    {
        character.SetSkinCollor(color);
    }
}
