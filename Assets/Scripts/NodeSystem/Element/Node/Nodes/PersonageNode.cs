using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class PersonageNode : Node
    {
        [InputPropperty]
        public Color skin = Color.white;

        [InputPropperty]
        public AppearanceItem hair;

        [InputPropperty]
        public AppearanceItem torso;

        [InputPropperty]
        public AppearanceItem legs;

        [InputPropperty]
        public AppearanceItem feet;

        public CharacterAppearance characterAppearance;

        public void CalculateChange()
        {
            if (skin != null)
            {
                characterAppearance.SetSkin(skin);
            }
            if (hair != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Hair, hair));
            }
            if (torso != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Torso, torso));
            }
            if (legs != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Legs, legs));
            }
            if (feet != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Feet, feet));
            }

            base.CalculateChange();
        }
    }
}