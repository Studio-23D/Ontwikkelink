using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class PersonageNode : Node
    {
        [InputPropperty]
        public GameObject hair;

        [InputPropperty]
        public GameObject torso;

        [InputPropperty]
        public GameObject legs;

        [InputPropperty]
        public GameObject feet;

        public CharacterAppearance characterAppearance;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);
            name = "Personage Node";

            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 10));

            rect.size = new Vector2(200, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height);
        }

        public override void Draw()
        {
            base.Draw();

            GUI.Box(nodeAreas[2], "", styleBottomArea);

            GUI.EndGroup();
        }

        public override void CalculateChange()
        {
            if (hair != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, GameObject>(AppearanceItemType.Hair, hair));
            }
            if (torso != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, GameObject>(AppearanceItemType.Torso, torso));
            }
            if (legs != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, GameObject>(AppearanceItemType.Legs, legs));
            }
            if (feet != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, GameObject>(AppearanceItemType.Feet, feet));
            }
            /*
            foreach(KeyValuePair<AppearanceItemType, GameObject> appearanceItem in appearanceItems)
            {
                if(appearanceItem.Value != null)
                {
                    characterAppearance.SetAppearanceItem(appearanceItem);
                }
            }*/
            base.CalculateChange();
        }
    }
}