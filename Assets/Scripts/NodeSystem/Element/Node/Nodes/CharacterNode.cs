using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class CharacterNode : Node
    {
        [InputPropperty]
        public Color huid = Color.white;

        [InputPropperty]
        public AppearanceItem haar;

        [InputPropperty]
        public AppearanceItem bovenstuk;

        [InputPropperty]
        public AppearanceItem onderstuk;

        [InputPropperty]
        public AppearanceItem schoenen;

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
            if (huid != null)
            {
                characterAppearance.SetSkin(huid);
            }
            if (haar != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Hair, haar));
            }
            if (bovenstuk != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Torso, bovenstuk));
            }
            if (onderstuk != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Legs, onderstuk));
            }
            if (schoenen != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Feet, schoenen));
            }
            base.CalculateChange();
        }
    }
}