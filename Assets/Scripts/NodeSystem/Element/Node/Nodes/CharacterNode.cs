using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class CharacterNode : Node
    {
        private List<Texture2D> icons = new List<Texture2D>();
        private List<Rect> iconPosition = new List<Rect>();

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
            name = "Personage";
            color = new Color32(255, 240, 193, 255);
            secondaireColor = new Color32(226, 171, 95, 255);

            connectionPointStartOffset = 90;
            connectionPointOffset = 40;

            height = 500;
            width = 180;

            icons.Add(Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Huid"));
            icons.Add(Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Haar"));
            icons.Add(Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Shirt"));
            icons.Add(Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Broek"));
            icons.Add(Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/icon_Shoenen"));

            base.Init(position, eventHandeler);

            for (int i = 0; i < icons.Count; i++)
            {
                iconPosition.Add(new Rect(NodePosition.x + NodeSize.x / 2 - 40, NodePosition.y + elementY, 80, 80));
                elementY += 80;
            }
        }

        public override void Draw()
        {
            base.Draw();

            for(int i = 0; i < icons.Count; i++)
            {
                GUI.DrawTexture(iconPosition[i], icons[i], ScaleMode.ScaleToFit, true);
            }

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