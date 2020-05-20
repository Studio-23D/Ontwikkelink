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

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);
            name = "Personage Node";
        }

        public override void Draw()
        {
            base.Draw();

            GUI.EndGroup();
        }

        public override void CalculateChange()
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